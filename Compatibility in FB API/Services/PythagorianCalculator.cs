using System;
using System.Collections.Generic;
using System.Linq;
using MatchUp.Models;
using MatchUp.Models.DBModels;

namespace MatchUp.Services
{
    public class PythagorianCalculator
    {
        public PythagorianMatrix CalculateSqare(DateTime birthday)
        {
            string stringBirthday = birthday.ToString("ddMMyyyy");
            string stringNumber = GetStringNumbers(stringBirthday);
            PythagorianMatrix matrix = GetResultMatrix(stringNumber);

            return matrix;
        }

        //Расчет главной матрицы
        public string GetStringNumbers(string dob)
        {
            int sum1 = int.Parse(dob[0].ToString()) +
                       int.Parse(dob[1].ToString()) +
                       int.Parse(dob[2].ToString()) +
                       int.Parse(dob[3].ToString());

            int sum2 = int.Parse(dob[4].ToString()) +
                       int.Parse(dob[5].ToString()) +
                       int.Parse(dob[6].ToString()) +
                       int.Parse(dob[7].ToString());

            int firstWorkNum = sum1 + sum2;

            string secondWorkNumStr = firstWorkNum.ToString();
            int secondWorkNum;

            try
            {
                secondWorkNum = int.Parse(secondWorkNumStr[0].ToString()) +
                                int.Parse(secondWorkNumStr[1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                secondWorkNum = int.Parse(secondWorkNumStr[0].ToString());
            }

            int thirfWorkNum = Math.Abs(firstWorkNum - GetFirstNum(dob) * 2);

            string forthWorkNumStr = thirfWorkNum.ToString();
            int forthWorkNum;

            try
            {
                forthWorkNum = int.Parse(forthWorkNumStr[0].ToString()) +
                               int.Parse(forthWorkNumStr[1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                forthWorkNum = int.Parse(forthWorkNumStr[0].ToString());
            }

            //table
            string result = null;

            result += CheckNum(firstWorkNum);
            result += CheckNum(secondWorkNum);
            result += CheckNum(thirfWorkNum);
            result += CheckNum(forthWorkNum);
            result += dob;

            return result;
        }

        //Расчет в процентном соотнешении основных качеств
        public Dictionary<int, int> GetPercent(PythagorianMatrix square)
        {
            Dictionary<int, int> persentSuare = new Dictionary<int, int>();

            persentSuare.Add(1, GetPersentIfZero(square.CharacterWill, MaxNumberInMatrix.ONE));
            persentSuare.Add(2, GetPersentIfZero(square.VitalEnergy, MaxNumberInMatrix.TWO));
            persentSuare.Add(3, GetPersentIfZero(square.CognitiveCreative, MaxNumberInMatrix.THREE));
            persentSuare.Add(4, GetPersentIfZero(square.HealthBeauty, MaxNumberInMatrix.FOUR));
            persentSuare.Add(5, GetPersentIfZero(square.LogicIntuition, MaxNumberInMatrix.FIVE));
            persentSuare.Add(6, GetPersentIfZero(square.LaborSkill, MaxNumberInMatrix.SIX));
            persentSuare.Add(7, GetPersentIfZero(square.Luck, MaxNumberInMatrix.SEVEN));
            persentSuare.Add(8, GetPersentIfZero(square.Duty, MaxNumberInMatrix.EIGHT));
            persentSuare.Add(9, GetPersentIfZero(square.IntellectMemory, MaxNumberInMatrix.NINE));

            return persentSuare;
        }

        //Secondary calculate
        public SecondaryAbilities CalculateSecondaryAbilities(PythagorianMatrix square)
        {
            SecondaryAbilities secondaryAbilities = new SecondaryAbilities
            {
                SelfEsteem = GetNumSecondaryAbilities(square.CharacterWill, square.VitalEnergy, square.CognitiveCreative),
                Moneymaking = GetNumSecondaryAbilities(square.HealthBeauty, square.LogicIntuition, square.LaborSkill),
                Talent = GetNumSecondaryAbilities(square.Luck, square.Duty, square.IntellectMemory),
                Tenacity = GetNumSecondaryAbilities(square.CharacterWill, square.HealthBeauty, square.Luck),
                DomesticBliss = GetNumSecondaryAbilities(square.VitalEnergy, square.LogicIntuition, square.Duty),
                Stability = GetNumSecondaryAbilities(square.CognitiveCreative, square.LaborSkill, square.IntellectMemory),
                Spirituality = GetNumSecondaryAbilities(square.CharacterWill, square.LogicIntuition, square.IntellectMemory),
                Temperament = GetNumSecondaryAbilities(square.CognitiveCreative, square.LogicIntuition, square.Luck)
            };

            return secondaryAbilities;
        }

        //Расчет дополнительных качеств
        public Dictionary<string, int> GetMoreInfo(Dictionary<int, string> square)
        {
            Dictionary<string, int> moreInfo = new Dictionary<string, int>();

            moreInfo.Add("Самооценка", GetNumSecondaryAbilities(square[1], square[2], square[3]));
            moreInfo.Add("Работоспособность", GetNumSecondaryAbilities(square[4], square[5], square[6]));
            moreInfo.Add("Талант", GetNumSecondaryAbilities(square[7], square[8], square[9]));
            moreInfo.Add("Целеустремленность", GetNumSecondaryAbilities(square[1], square[4], square[7]));
            moreInfo.Add("Семейность", GetNumSecondaryAbilities(square[2], square[5], square[8]));
            moreInfo.Add("Привычки", GetNumSecondaryAbilities(square[3], square[6], square[9]));
            moreInfo.Add("Темперамент", GetNumSecondaryAbilities(square[3], square[5], square[7]));
            moreInfo.Add("Духовность", GetNumSecondaryAbilities(square[1], square[5], square[9]));

            return moreInfo;
        }

        public Dictionary<string, int> GetMoreInfoInPercent(Dictionary<string, int> moreInfoSquare)
        {
            Dictionary<string, int> moreInfo = new Dictionary<string, int>();
            
            moreInfo.Add("Самооценка", GetNumMoreInfoSquareInPercent(moreInfoSquare["Самооценка"], MaxNumberInMoreInfo.One));
            moreInfo.Add("Работоспособность", GetNumMoreInfoSquareInPercent(moreInfoSquare["Работоспособность"], MaxNumberInMoreInfo.Two));
            moreInfo.Add("Талант", GetNumMoreInfoSquareInPercent(moreInfoSquare["Талант"], MaxNumberInMoreInfo.Three));
            moreInfo.Add("Целеустремленность", GetNumMoreInfoSquareInPercent(moreInfoSquare["Целеустремленность"], MaxNumberInMoreInfo.Four));
            moreInfo.Add("Семейность", GetNumMoreInfoSquareInPercent(moreInfoSquare["Семейность"], MaxNumberInMoreInfo.Five));
            moreInfo.Add("Привычки", GetNumMoreInfoSquareInPercent(moreInfoSquare["Привычки"], MaxNumberInMoreInfo.Six));
            moreInfo.Add("Темперамент", GetNumMoreInfoSquareInPercent(moreInfoSquare["Темперамент"], MaxNumberInMoreInfo.Seven));
            moreInfo.Add("Духовность", GetNumMoreInfoSquareInPercent(moreInfoSquare["Духовность"], MaxNumberInMoreInfo.Eight));

            return moreInfo;
        }

        public int CalculateCompare(UserViewModel model1, UserViewModel model2)
        {
            float similar = 0;

            int counter = 0;

            foreach (var i in model1.SquarePersent)
            {
                if (i.Value == model2.SquarePersent[i.Key])
                {
                    counter++;
                }
                //similar += Math.Abs(i.Value - model2.SquarePersent[i.Key]);
            }

            similar += (float)counter / 9 * 100;
            
            //similar = similar / 9;
            //similar = 100 - similar;

            return (int)similar;
        }

        public void CalculateAll(UserViewModel model)
        {
            //model.Matrix = GetSqare(model);
            //model.SquarePersent = GetPercent(model.Matrix);
            //model.MoreInfo = GetMoreInfo(model.Matrix);
            //model.MoreInfoPersent = GetMoreInfoInPercent(model.MoreInfo);
            //model.Descriptions = GetDiscriptions(model).ToList();
        }

        #region PrivateMethod
        
        private int GetNumMoreInfoSquareInPercent(int number, int maxNum)
        {
            if (number == 0)
            {
                return 0;
            }

            var persent = (float)number / maxNum * 100;

            return (int)persent;
        }

        private int GetNumSecondaryAbilities(string a, string b, string c)
        {
            int numNumber = GetNumberNum(a) + GetNumberNum(b) + GetNumberNum(c);

            return numNumber;
        }

        private int GetNumberNum(string number)
        {
            if (number == "-")
            {
                return 0;
            }

            return number.Length;
        }

        private int GetPersentIfZero(string numNum, int numMax)
        {
            if (numNum == "-")
            {
                return 0;
            }

            var persent = (float)numNum.Length / numMax * 100;

            return (int)persent;
        }

        private string CheckNum(int num)
        {
            if (num <= 9)
            {
                return string.Concat("0", num);
            }

            return string.Concat(num);
        }

        private PythagorianMatrix GetResultMatrix(string result)
        {
            PythagorianMatrix matrix = new PythagorianMatrix
            {
                CharacterWill = Parse(result, 1),
                VitalEnergy = Parse(result, 2),
                CognitiveCreative = Parse(result, 3),
                HealthBeauty = Parse(result, 4),
                LogicIntuition = Parse(result, 5),
                LaborSkill = Parse(result, 6),
                Luck = Parse(result, 7),
                Duty = Parse(result, 8),
                IntellectMemory = Parse(result, 9)
            };

            return matrix;
        }

        private string Parse(string stringNumbers, int number)
        {
            char charNumber = number.ToString()[0];
            
            int numNumbers = stringNumbers.Count(x => x == charNumber);

            string result = "";

            for (int i = 0; i < numNumbers; i++)
            {
                result += number.ToString();
            }

            return result == "" ? "-" : result;
        }

        private int GetFirstNum(string dob)
        {
            if (dob[0] == '0')
            {
                return int.Parse(dob[1].ToString());
            }

            return int.Parse(dob[0].ToString());
        }

        #endregion

        private class MaxNumberInMatrix
        {
            public const int ONE = 6;
            public const int TWO = 4;
            public const int THREE = 4;
            public const int FOUR = 3;
            public const int FIVE = 4;
            public const int SIX = 4;
            public const int SEVEN = 4;
            public const int EIGHT = 4;
            public const int NINE = 4;
        }

        private static class MaxNumberInMoreInfo
        {
            public static int One = 14;
            public static int Two = 11;
            public static int Three = 12;
            public static int Four = 13;
            public static int Five = 12;
            public static int Six = 12;
            public static int Seven = 12;
            public static int Eight = 14;
        }
    }
}