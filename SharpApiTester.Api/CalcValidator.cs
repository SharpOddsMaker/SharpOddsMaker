using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApiTester.Api
{
    public class CalcValidator
    {
        public string ErrMessage { get; private set; }

        public bool ValidateCalcInputs(string favoriteTypeStr, string matchOddFavStr, string goalsOver2_5Str, string drawStr, string profitIdStr, int calcType)
        {
            ErrMessage = string.Empty;

            byte favoriteType;
            decimal matchFavOdd, totalGoals3p, draw;
            int profitId;

            bool isFavoriteType = byte.TryParse(favoriteTypeStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out favoriteType);
            bool isMatchOddFavOk = decimal.TryParse(matchOddFavStr, NumberStyles.Float, CultureInfo.InvariantCulture, out matchFavOdd);
            bool isTotalGoals3pOk = decimal.TryParse(goalsOver2_5Str, NumberStyles.Float, CultureInfo.InvariantCulture, out totalGoals3p);
            bool isDrawOk = decimal.TryParse(drawStr, NumberStyles.Float, CultureInfo.InvariantCulture, out draw);
            bool isProfitIdOk = int.TryParse(profitIdStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out profitId);

            if (string.IsNullOrEmpty(favoriteTypeStr))
                AddErrorMsg("* Favorite Type on Match is Required. Which team is favorite on match (has smaller Match odd)? 1 (Home) or 2 (Away)");
            else if (isFavoriteType == false)
                AddErrorMsg("* Favorite Type on Match should be a number 1 (Home) or 2 (Away). Team that is a favorite on match has smaller Match Winner odd.");
            else if (isFavoriteType == true && !(favoriteType == 1 || favoriteType == 2))
                AddErrorMsg("* Favorite Type on Match is Out of Range. Should be 1 (Home) or 2 (Away). Team that is a favorite on match has smaller Match Winner odd.");

            if (string.IsNullOrEmpty(matchOddFavStr))
                AddErrorMsg("* Match Winner on Favorite decimal Odd is required. Take smaller of Home or Away decimal odd.");
            else if (isMatchOddFavOk == false)
                AddErrorMsg("* Match Winner Odd on Favorite should be a decimal number");
            else if (isMatchOddFavOk && matchFavOdd < 1 || matchFavOdd > 10)
                AddErrorMsg("* Match Winner Odd on Favirite is Out of Range (1-10)");

            if (string.IsNullOrEmpty(goalsOver2_5Str))
                AddErrorMsg("Total Goals Over 2.5 decimal odd is required");
            else if (isTotalGoals3pOk == false)
                AddErrorMsg("Total Goals Over 2.5 should be a decimal number");
            else if (isTotalGoals3pOk && totalGoals3p < 1 || totalGoals3p > 40)
                AddErrorMsg("* Total Goals Over 2.5 is Out of Range (1-40)");

            if (string.IsNullOrEmpty(drawStr))
                AddErrorMsg("* Draw decimal odd is required");
            else if (isDrawOk == false)
                AddErrorMsg("* Draw odd should be a decimal number");
            else if (isDrawOk && draw < 1 || draw > 30)
                AddErrorMsg("* Draw Odd is Out of Range (1-30)");

            if (string.IsNullOrEmpty(profitIdStr))
                AddErrorMsg("* ProfitId is Required.");
            else if (isProfitIdOk == false)
                AddErrorMsg("* ProfitId should be a number");

            if (calcType == 0)
                AddErrorMsg("* Calc Type is Required.");
            else if (calcType >= 4)
                AddErrorMsg("* Calc Type should be in range 1 - 3");

            if (ErrMessage.Length > 2)
                return false;
            else
                return true;
        }


        private void AddErrorMsg(string message)
        {
            ErrMessage += $"{message}{Environment.NewLine}";
        }

    }
}
