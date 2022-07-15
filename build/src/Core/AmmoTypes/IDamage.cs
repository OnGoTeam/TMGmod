using System;
using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public interface IDamage
    {
        float DamageMean { get; }
        float DamageVariation { get; }
        float DistanceConvexity { get; }
        float AlphaDamage { get; }
    }

    public static class Damage
    {
        private static float GetMean(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.DamageMean : ammo is ATShrapnel ? 30f : 50f;
        }

        private static float GetVariation(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.DamageVariation : ammo is ATShrapnel ? 1f : 0.5f;
        }

        private static float GetConvexity(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.DistanceConvexity : ammo is ATShrapnel ? 1f : 0f;
        }

        private static float GetAlpha(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.AlphaDamage : ammo is ATShrapnel ? 0.8f : 0.01f;
        }

        private static float CalculateBase(AmmoType ammo)
        {
            var variation = GetVariation(ammo);
            return Rando.Float(1 - variation, 1 + variation) * GetMean(ammo);
        }

        private static double Clamp(double value)
        {
            return Math.Max(0.0, Math.Min(1.0, value));
        }

        private static double Square(double value)
        {
            return value * value;
        }

        private static double PositiveCoefficient(double alpha, double distanceProportion)
        {
            if (!(Math.Abs((alpha - 1) * alpha) > 0)) return alpha;
            // else
            var k = (Math.Sqrt(alpha) + alpha) / (1 - alpha);
            return Square(k / (distanceProportion + k));
        }

        private static double NegativeCoefficient(double alpha, double distanceProportion)
        {
            return 1 - (1 - alpha) * Square(distanceProportion);
        }

        private static double WeightedMean(
            double left, double right,
            double leftc, double rightc
        )
        {
            return (leftc * left + rightc * right) / (leftc + rightc);
        }

        private static double CalculateCoeff(double alpha, double convexity, double distanceProportion)
        {
            return Clamp(
                WeightedMean(
                    PositiveCoefficient(alpha, distanceProportion),
                    NegativeCoefficient(alpha, distanceProportion),
                    Math.Exp(+convexity),
                    Math.Exp(-convexity)
                )
            );
        }

        public static float CalculateCoeff(AmmoType ammo, float distanceProportion)
        {
            return (float)CalculateCoeff(GetAlpha(ammo), GetConvexity(ammo), Clamp(distanceProportion));
        }

        private static float CalculateCoeff(Bullet bullet)
        {
            return CalculateCoeff(bullet.ammo, bullet.bulletDistance / Math.Max(1, bullet.ammo.range));
        }

        public static float Calculate(Bullet bullet)
        {
            return CalculateBase(bullet.ammo) * CalculateCoeff(bullet);
        }
    }
#if DEBUG
    public class StringMarker : Thing
    {
        private readonly string _string;
        private float _alive = 1f;

        private StringMarker(Vec2 pos, string @string) : base(pos.x, pos.y)
        {
            _string = @string;
            _hSpeed = Rando.Float(-1f, 1f);
            _vSpeed = Rando.Float(-.5f, 1f);
        }

        public override void Update()
        {
            x += hSpeed;
            y += vSpeed;
            _alive -= 0.05f;
            if (_alive < 0) Level.Remove(this);
        }

        public override void Draw()
        {
            Graphics.DrawString(_string, position, new Color(Rando.Int(192, 255), Rando.Int(0, 63), 0));
        }

        public static void Show(Vec2 position, string @string)
        {
            Level.Add(new StringMarker(position, @string));
        }
    }

    public class DotMarker : Thing
    {
        private DotMarker(Vec2 position) : base(position.x, position.y)
        {
        }

        public override void Draw()
        {
            Graphics.DrawCircle(position, 2, Color.Purple);
        }

        public static void Show(Vec2 position)
        {
            Level.Add(new DotMarker(position));
        }
    }

    public class StrokeMarker : Thing
    {
        private readonly Vec2 _pos2;

        private StrokeMarker(Vec2 position, Vec2 pos2) : base(position.x, position.y)
        {
            _pos2 = pos2;
        }

        public override void Draw()
        {
            Graphics.DrawLine(position, _pos2, Color.Gold);
        }

        public static void Show(Vec2 position, Vec2 pos2)
        {
            Level.Add(new StrokeMarker(position, pos2));
        }
    }
#endif
}
