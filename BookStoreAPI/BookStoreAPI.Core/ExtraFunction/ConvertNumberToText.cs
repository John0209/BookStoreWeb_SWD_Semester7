using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Function
{
    public class ConvertNumberToText
    {
        public string NumberToText(double inputNumber)
        {
            string[] unitNumber = new string[] {" không "," một", " hai", " ba", " bốn", " năm", " sáu",
            " bảy", " tám", " chín"};
            string[] placeValue = new string[] { " ", " nghìn", " triệu", " tỷ" };
            bool isNegative = false;

            //convert, nếu kiểu double 123.1111 thì chỉ lấy trước dấu phẩy = 123
            string sNumber = inputNumber.ToString("#");
            if (sNumber.Length == 0)
            {
                return "không đồng";
            }
            double number = Convert.ToDouble(sNumber);
            //check số âm
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }

            int one, ten, hundred;
            int positionDigit = sNumber.Length;
            string result = " ";
            int place = 0;

            if (positionDigit == 0)
            {
                result = unitNumber[0] + result;
            }
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###

                while (positionDigit > 0)
                {
                    one = ten = hundred = -1;
                    one = Int32.Parse(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        ten = Int32.Parse(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                    }
                    if (positionDigit > 0)
                    {
                        hundred = Int32.Parse(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                    }
                    //place Value
                    if (one > 0 || ten > 0 || hundred > 0 || place == 3)
                    {
                        result = placeValue[place] + result;
                    }
                    place++;
                    if (place > 3) { place = 1; }
                    //one
                    if (one == 5 && ten > 0)
                    {
                        result = " lăm" + result;
                    }
                    else if (one == 1 & ten > 1)
                    {
                        result = " mốt" + result;
                    }
                    else if (one > 0)
                    {
                        result = unitNumber[one] + result;
                    }
                    //ten
                    if (ten == 0 && one > 0)
                    {
                        result = " lẻ" + result;
                    }
                    if (ten == 1)
                    {
                        result = " mười" + result;
                    }
                    if (ten > 1)
                    {
                        result = unitNumber[ten] + " mươi" + result;
                    }
                    //hundred
                    if (hundred > 0)
                    {
                        result = unitNumber[hundred] + " trăm" + result;
                    }

                }//end white
                result = result.Trim();
                if (isNegative)
                {
                    result = "âm " + result;
                }
                result = result + " đồng";
            }//end if else chính
            return result;
        }//end function
    }
}
