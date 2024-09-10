namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterLightAttackDeriveData : CharacterDeriveData
    {
        #region General
        internal int layer = 1;
        internal bool rushAttack = false;
        #endregion
    }

    internal class AnbyDemaraLightAttackDeriveData : CharacterLightAttackDeriveData
    {
        #region AnbyDemara
        internal bool delayDerive = false;
        internal bool derivedFromHeavyAttack = false;
        #endregion
    }
}