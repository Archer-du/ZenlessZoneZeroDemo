namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    // TODO: 明显有设计问题，之后再改
    internal class CharacterEvadeDeriveData : CharacterDeriveData
    {
        internal enum DeriveType
        {
            Reentrant,
            None
        }
        internal static CharacterEvadeDeriveData defaultData = new(DeriveType.Reentrant);
        internal DeriveType type;
        internal CharacterEvadeDeriveData(DeriveType type)
        {
            this.type = type;
        }

        public CharacterEvadeDeriveData()
        {
            type = DeriveType.None;
        }
    }
}