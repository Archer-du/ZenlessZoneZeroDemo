namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterEvadeDeriveData : CharacterDeriveData
    {
        internal enum DeriveType
        {
            Reentrant,
        }
        internal DeriveType type;
        internal CharacterEvadeDeriveData(DeriveType type)
        {
            this.type = type;
        }

    }
}