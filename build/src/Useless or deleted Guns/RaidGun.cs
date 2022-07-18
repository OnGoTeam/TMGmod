#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;

// ReSharper disable VirtualMemberCallInConstructor


namespace TMGmod.Useless_or_deleted_Guns
{
    [EditorGroup("TMG|DEBUG")]
    [BaggedProperty("isInDemo", true)]
    [BaggedProperty("canSpawn", true)]
    [PublicAPI]
    [Obsolete]
    public class RaidGun : Gun
    {
        private float _dwait;
        private List<string> _log = new List<string>();

        public RaidGun(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 60000;
            _ammoType = new ATShotgun
            {
                accuracy = 0.9f,
                range = 250f,
                penetration = 5f,
            };
            _type = "gun";
            graphic = new Sprite(GetPath("RaidGun"));
            center = new Vec2(12f, 3f);
            collisionOffset = new Vec2(-12f, -3f);
            collisionSize = new Vec2(24f, 6f);
            _barrelOffsetTL = new Vec2(23f, 2f);
            _fireSound = "shotgun";
            _fullAuto = true;
            _fireWait = 0.001f;
            _kickForce = 0.9f;
            _holdOffset = new Vec2(0f, 2f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "RaidGun";
            _numBulletsPerFire = 5;
        }

        private const int MaxLen = 10;

        public override void Update()
        {
            if (duck != null)
            {
                foreach (var key in new[] { "UP", "DOWN", "LEFT", "RIGHT", "QUACK", "RAGDOLL" })
                {
                    if (duck.inputProfile.Pressed(key)) _log.Add(key);
                    if (_log.Count > MaxLen)
                        _log.RemoveRange(0, _log.Count - MaxLen);
                }

                if (
                    _log.SequenceEqual(
                        new[]
                        {
                            "UP",
                            "UP",
                            "DOWN",
                            "DOWN",
                            "LEFT",
                            "RIGHT",
                            "LEFT",
                            "RIGHT",
                            "QUACK",
                            "RAGDOLL",
                        }
                    )
                ) duck.Kill(new DTFall());
            }

            base.Update();
            _dwait -= 1f / 60f;
            if (_dwait < 0) _dwait = 0;
        }

        public override void Fire()
        {
            while (_dwait <= 1f / 60f)
            {
                _dwait += Math.Max(_fireWait, 0.00001f);
                _wait = 0;
                base.Fire();
            }
        }

        public override void Reload(bool shell = true)
        {
            base.Reload(false);
        }
    }
}
#endif
