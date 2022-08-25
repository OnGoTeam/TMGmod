namespace TMGmod.Core.Modifiers.Updating
{
    public struct DeploymentState
    {
        public readonly bool Folded;
        public readonly bool Deployed;
        public readonly float State;

        public DeploymentState(bool deployed, bool folded, float state)
        {
            Deployed = deployed;
            Folded = folded;
            State = state;
        }
    }
}
