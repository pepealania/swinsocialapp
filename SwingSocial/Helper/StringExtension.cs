using SwingSocial.Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class StringExtension
    {
        public static string GetBirthGender(this string BirthGender,ProfileEntity profile,Page page)
        {
            string myBirthGender = String.Empty;
            try
            {
                myBirthGender = string.Concat(myBirthGender.GetAge(profile.DateOfBirth), $"{profile.Gender.Substring(0, 1)}");
                if (profile.PartnerGender != null && profile.PartnerGender!=String.Empty)
                {
                    if (profile.PartnerDateOfBirth != null)
                    {
                        myBirthGender = string.Concat(myBirthGender, $" | {myBirthGender.GetAge(profile.PartnerDateOfBirth)}");
                        myBirthGender = string.Concat(myBirthGender, $"{profile.PartnerGender.Substring(0, 1)}");

                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                page.DisplayAlert("System Data Error","Data Error (GetBirthGender)","ok");
                throw;
            }

            return myBirthGender;
        }
        public static string GetBirthGender(this string BirthGender, ProfileEntity profile)
        {
            string myBirthGender = String.Empty;
            try
            {
                myBirthGender = string.Concat(myBirthGender.GetAge(profile.DateOfBirth), $"{profile.Gender.Substring(0, 1)}");
                if (profile.PartnerGender != null && profile.PartnerGender != String.Empty)
                {
                    if (profile.PartnerDateOfBirth != null)
                    {
                        myBirthGender = string.Concat(myBirthGender, $" | {myBirthGender.GetAge(profile.PartnerDateOfBirth)}");
                        myBirthGender = string.Concat(myBirthGender, $"{profile.PartnerGender.Substring(0, 1)}");

                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return myBirthGender;
        }

        public static string GetAge(this string dateOfBirth, DateTime? dob)
        {
            try
            {
                int myCalculatedAge = int.MinValue;
                myCalculatedAge = DateTime.Today.Year - dob.Value.Year;
                if (dob.Value.AddYears(myCalculatedAge) > DateTime.Today)
                {
                    myCalculatedAge--;
                }
                return myCalculatedAge.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }        
        }

        public static string PartnerGetBirthGender(this string BirthGender, ProfileEntity profile)
        {
            string myPartnerBirthGender = String.Empty;
            try
            {
                myPartnerBirthGender = string.Concat(myPartnerBirthGender.PartnerGetAge(profile.PartnerDateOfBirth), $"{profile.PartnerGender.Substring(0, 1)}");

            }
            catch (Exception ex)
            {

                throw;
            }

            return myPartnerBirthGender;
        }

        public static string PartnerGetAge(this string dateOfBirth, DateTime? dob)
        {
            try
            {
                if (dob==null)
                {
                    return "0";
                }
                int myCalculatedPartnerAge = int.MinValue;
                myCalculatedPartnerAge = DateTime.Today.Year - dob.Value.Year;
                if (dob.Value.AddYears(myCalculatedPartnerAge) > DateTime.Today)
                {
                    myCalculatedPartnerAge--;
                }
                return myCalculatedPartnerAge.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public static string GetHeightString(this string something, int height)
        {
                double feetHeight = Math.Round(height / 12.0, 2);
                return feetHeight.ToString() + " feet";
        }

        public static string GetIdString(this string something, Guid Id)
        {
            return Id.ToString();           
        }
    }
}
