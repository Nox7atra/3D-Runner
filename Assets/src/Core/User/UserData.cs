using UnityEngine;
using System;
using System.IO;
using System.Xml;
namespace Runner.Core.User {
    public class UserData
    {
        private const string USER_FILE_NAME = "user.xml";
        #region singletone description
        private static UserData _Instance;
        public static UserData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserData();
                }
                return _Instance;
            }
        }
        private UserData()
        {
            _UserDataPathWithoutFilename = Application.persistentDataPath
                + "/" + PathConstants.USER_DIRECTORY_PATH;
            _UserDataPath = Application.persistentDataPath 
                + "/" + PathConstants.USER_DIRECTORY_PATH + USER_FILE_NAME;
            Load();
        }
        #endregion
        private const float DISTANCE_TO_SCORE_EXCHANGE_RATE = 5f;
        private string _UserDataPath;
        private string _UserDataPathWithoutFilename;
        private int _CurrentScore;
        private int _BestScore;
        public int CurrentScore
        {
            get
            {
                return _CurrentScore;
            }
        }
        public int BestScore
        {
            get
            {
                return _BestScore;
            }
            set
            {
                _BestScore = value;
            }
        }
        private void CreateDefaultUser()
        {
            //Read default user
            TextAsset textAsset = (TextAsset)Resources.Load(
                PathConstants.USER_DEFAULT, typeof(TextAsset));
            StringReader reader = new StringReader(textAsset.text);
            XmlDocument user = new XmlDocument();
            user.Load(reader);
            foreach (XmlNode node in user.SelectNodes("User"))
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "BestScore":
                            _BestScore
                                = Convert.ToInt32(child.Attributes["Value"].Value);
                            break;
                    }
                }
            }
            //Create user file
            try
            {
                if (!Directory.Exists(_UserDataPath))
                {
                    Directory.CreateDirectory(_UserDataPathWithoutFilename);
                    File.Create(_UserDataPath).Close();
                }

            }
            catch (IOException ex)
            {
                Debug.Log(ex.Message);
            }
            user.Save(_UserDataPath);
            reader.Close();
        }
        private void ReadCurrentUser()
        {
            XmlDocument user = new XmlDocument();
            user.Load(_UserDataPath);
            foreach (XmlNode node in user.SelectNodes("User"))
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "BestScore":
                            _BestScore
                                = Convert.ToInt32(child.Attributes["Value"].Value);
                            break;
                    }
                }
            }
        }
        private void Load()
        {
            if (File.Exists(_UserDataPath))
            {
                ReadCurrentUser();
            }
            else
            {
                CreateDefaultUser();
            }
        }
        public void Save()
        {
            XmlDocument user = new XmlDocument();
            user.Load(_UserDataPath);
            foreach (XmlNode node in user.SelectNodes("User"))
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "BestScore":
                            child.Attributes["Value"].Value
                                = _BestScore.ToString();
                            break;
                    }
                }
            }
            user.Save(_UserDataPath);
        }
        public void SetCurrentScore(float distance)
        {
            _CurrentScore = DistanceToScoreExchange(distance);
        }
        public void ApplyBestScore()
        {
            if(_CurrentScore > _BestScore)
            {
                _BestScore = _CurrentScore;
                Save();
            }

        }
        private int DistanceToScoreExchange(float distance)
        {
            return (int)(distance * DISTANCE_TO_SCORE_EXCHANGE_RATE);
        }
    }
}