namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterLightAttackDeriveData : CharacterDeriveData
    {
        internal static CharacterLightAttackDeriveData defaultData = new(1, false, false);
        internal int deriveLayer;
        internal bool rushAttack;
        internal bool delayDerive;
        public CharacterLightAttackDeriveData(int deriveLayer, bool rushAttack = false, bool delayDerive = false)
        {
            this.deriveLayer = deriveLayer;
            this.rushAttack = rushAttack;
            this.delayDerive = delayDerive;
        }

        public CharacterLightAttackDeriveData()
        {
            this.deriveLayer = 1;
            this.rushAttack = false;
            this.delayDerive = false;
        }
    }
}