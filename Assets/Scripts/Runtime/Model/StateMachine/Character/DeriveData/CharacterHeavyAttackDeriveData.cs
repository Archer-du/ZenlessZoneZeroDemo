namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterHeavyAttackDeriveData : CharacterDeriveData
    {
        internal static CharacterHeavyAttackDeriveData defaultData = new();
        internal bool perfectDerive;
        public CharacterHeavyAttackDeriveData(bool perfectDerive)
        {
            this.perfectDerive = perfectDerive;
        }
        public CharacterHeavyAttackDeriveData()
        {
            perfectDerive = false;
        }
    }
}