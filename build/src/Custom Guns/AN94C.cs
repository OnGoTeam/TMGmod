﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseBurst, IHspeedKforce, IAmAr, IHaveSkin
    {
        public bool Laserino;
        public StateBinding StockBinding = new StateBinding(nameof(Laserino));
        // ReSharper disable once MemberCanBePrivate.Global
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Fid;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6, 7 });
        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            Fid = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("AN94Cpattern"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 3f);
            _holdOffset = new Vec2(3f, 2f);
            ammo = 30;
            _ammoType = new ATMagnum { range = 260f, bulletSpeed = 180f };
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.1f;
            Kforce1Ar = 0.07f;
            _kickForce = 0.9f;
            Kforce2Ar = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _editorName = "AN94 with Wooden Stock";
            _weight = 5.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            DeltaWait = 0.07f;
            BurstNum = 2;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Laserino)
                {
                    laserSight = false;
                    _weight = 5.5f;
                    _sprite.frame -= 10;
                    Laserino = false;
                }
                else
                {
                    laserSight = true;
                    _weight = 6f;
                    _sprite.frame += 10;
                    Laserino = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        private void UpdateSkin()
        {
            var fid = Fid.value;
            while (!Allowedlst.Contains(fid))
            {
                fid = Rando.Int(0, 9);
            }
            _sprite.frame = fid;
        }
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
        public float Kforce1Ar { get; }
        public float Kforce2Ar { get; }
    }
}