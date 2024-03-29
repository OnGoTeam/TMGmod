﻿using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses.ClassImplementations
{
    public abstract class BaseSmg : BaseGun, IAmSmg
    {
        private readonly FirstKforce _firstKforce;

        protected BaseSmg(float xval, float yval) : base(xval, yval)
        {
            KforceDelta = 0.2f;
            _firstKforce = new FirstKforce(50, kforce => kforce + KforceDelta);
            _fullAuto = true;
            Compose(_firstKforce);
        }

        protected float KforceDelta { get; set; }

        protected uint KforceDelay
        {
            set => _firstKforce.MaxDelay = value;
        }
    }
}
