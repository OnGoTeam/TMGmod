using DuckGame;
using JetBrains.Annotations;
using System.Collections.Generic;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class IB8mm : BaseGun, IFirstPrecise, IFirstKforce, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public IB8mm(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new ATIB8();
            BaseAccuracy = 0.82f;
            MinAccuracy = 0.24f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("IB-8mm Sniper"), 28, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(14f, 6f);
            _collisionOffset = new Vec2(-14f, -6f);
            _collisionSize = new Vec2(28f, 12f);
            _barrelOffsetTL = new Vec2(28f, 5f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.5f;
            KickForceDeltaSmg = 2.1f;
            MaxDelaySmg = 11;
            MaxAccuracyFp = 1f;
            MaxDelayFp = 20;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.15f;
            _holdOffset = new Vec2(-2f, 0f);
            ShellOffset = new Vec2(-3f, 0f);
            _editorName = "IB-8mm Sniper";
            _weight = 3f;
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public float KickForceDeltaSmg { get; }
        public int CurrentDelaySmg { get; set; }
        public int MaxDelaySmg { get; }
        public int CurrentDelayFp { get; set; }
        public int MaxDelayFp { get; }
        public float MaxAccuracyFp { get; }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}