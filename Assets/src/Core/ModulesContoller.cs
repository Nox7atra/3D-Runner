namespace Runner.Core
{
    public class ModulesContoller
    {
        #region singletone description
        private static ModulesContoller _Instance;

        public static ModulesContoller Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModulesContoller();
                }
                return _Instance;
            }
        }
       
        private ModulesContoller()
        {

        }
        #endregion

        public Modules Modules;
    }
}
