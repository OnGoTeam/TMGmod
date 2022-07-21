﻿using DuckGame;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class ANP73 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 4;
        private readonly SpriteMap _sprite;

        public ANP73(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 33;
            _ammoType = new ATANP73();
            IntrinsicAccuracy = true;
            
            _sprite = new SpriteMap(GetPath("Experimental ANP-73"), 19, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(10f, 7f);
            _collisionOffset = new Vec2(-10f, -7f);
            _collisionSize = new Vec2(19f, 14f);
            _barrelOffsetTL = new Vec2(19f, 3f);
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-3f, -3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 1.5f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            _editorName = "Experimental ANP-73";
            _weight = 2f;
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (_sprite.frame / 10 < 1) //1
                {
                    _sprite.frame += 10;
                    _fireWait = 1.2f;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.5f;
                }
                else if (_sprite.frame / 10 < 2) //2
                {
                    _sprite.frame += 10;
                    _fireWait = 0.9f;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.6f;
                }
                else if (_sprite.frame / 10 < 3) //3
                {
                    _sprite.frame += 10;
                    _fireWait = 0.6f;
                    loseAccuracy = 0.3f;
                    maxAccuracyLost = 0.7f;
                }
                else if (_sprite.frame / 10 < 4) //0
                {
                    _sprite.frame -= 30;
                    _fireWait = 1.5f;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.3f;
                }

                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}
