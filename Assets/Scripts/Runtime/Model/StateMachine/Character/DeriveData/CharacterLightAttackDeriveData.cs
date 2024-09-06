namespace ZZZDemo.Runtime.Model.StateMachine.Character.DeriveData
{
    internal class CharacterLightAttackDeriveData : CharacterDeriveData
    {
        internal int deriveLayer;
        internal bool rushAttack;
        public CharacterLightAttackDeriveData(int deriveLayer, bool rushAttack = false)
        {
            this.deriveLayer = deriveLayer;
            this.rushAttack = rushAttack;
        }
    }
}