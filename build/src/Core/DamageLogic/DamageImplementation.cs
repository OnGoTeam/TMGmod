using System;
using DuckGame;

namespace TMGmod.Core.DamageLogic
{
    public static class DamageImplementation
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
#if DEBUG
        public static float CalculateCoeff(AmmoType ammo, float distanceProportion)
#else
        private static float CalculateCoeff(AmmoType ammo, float distanceProportion)
#endif
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
}
