namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterLightAttackDeriveData : CharacterDeriveData
    {
        internal static CharacterLightAttackDeriveData defaultData = new(1, false, false);
        internal int layer;
        internal bool rushAttack;
        internal bool delayDerive;
        internal bool derivedFromHeavyAttack;
        public CharacterLightAttackDeriveData(
            int deriveLayer, 
            bool rushAttack = false, 
            bool delayDerive = false, 
            bool derivedFromHeavyAttack = false
            )
        {
            this.layer = deriveLayer;
            this.rushAttack = rushAttack;
            this.delayDerive = delayDerive;
            this.derivedFromHeavyAttack = derivedFromHeavyAttack;
        }

        public CharacterLightAttackDeriveData()
        {
            this.layer = 1;
            this.rushAttack = false;
            this.delayDerive = false;
            this.derivedFromHeavyAttack = false;
        }
    }
}