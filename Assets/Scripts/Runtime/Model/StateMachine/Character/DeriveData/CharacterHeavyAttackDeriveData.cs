namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterHeavyAttackDeriveData : CharacterDeriveData
    {
        internal static CharacterHeavyAttackDeriveData defaultData = new();
        internal bool perfectDerive;
        internal bool derivedFromDelayAttack;
        public CharacterHeavyAttackDeriveData(bool perfectDerive, bool derivedFromDelayAttack = false)
        {
            this.perfectDerive = perfectDerive;
            this.derivedFromDelayAttack = derivedFromDelayAttack;
        }
        public CharacterHeavyAttackDeriveData()
        {
            perfectDerive = false;
            this.derivedFromDelayAttack = false;
        }
    }
}