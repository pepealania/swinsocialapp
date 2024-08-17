using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class ProfileEntity
    {
        public bool pineappleVisible { get; set; }
        public bool pineappleCrossVisible { get; set; }

        public Guid ProfileId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public int? PartnerAge { get; set; }
        //public Gender Gender { get; set; }

        public string Photo { get; set; }

        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Username { get; set; }

        public string SexualOrientation { get; set; }
        public string PartnerSexualOrientation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? PartnerDateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Type { get; set; }
        public string PartnerGender { get; set; }
        public string BirthGender { get; set; }
        public string Location { get; set; }
        public int? Height { get; set; }
        public string HeightString { get; set; }
        public string BodyType { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public int? PartnerHeight { get; set; }
        public string PartnerHeightString { get; set; }
        public string PartnerBodyType { get; set; }
        public string PartnerEyeColor { get; set; }
        public string PartnerHairColor { get; set; }
        public string IdString { get; set; }
        public string About { get; set; }
        public string Tagline { get; set; }
        public string ProfileBanner { get; set; }
        public string AccountType { get; set; }
        public string[] SwingStyleTags { get; set; }
        public string[] LookingForTags { get; set; }

        public int LocationLength { get; internal set; }
        public int AccountTypeLength { get; internal set; }
        public  List<SwingStyleTagsObject> SwingStyleTagsObjectList { get; set; }

        public List<Event> Events{ get; set; }
        public List<MyImage> MyImages { get; set; }
        public bool TagLineIsVisible { get; internal set; }
        public bool SwingStyleTagsObjectListIsVisible { get; internal set; }
        public bool AboutIsVisible { get; internal set; }
        public HtmlWebViewSource DetailsHtmlTable { get; internal set; }
        public HtmlWebViewSource PartnerDetailsHtmlTable { get; internal set; }
        public bool IsGenderDetailsHtmlTableVisible { get; internal set; }
        public string Category { get; set; }
        public int Pineapple { get; set; }
        public string Distance { get; set; }
        public string Email { get; set; }
        public string Phone { get; internal set; }
        public string Password { get; internal set; }
        public string ConfirmPassword { get; internal set; }
        public string UserType { get; internal set; }
        public string[] Desires { get; internal set; }

        public string Fantasies { get; internal set; }
        public int HeightInt { get; internal set; }
        public int AgeInt { get; internal set; }
        public string LastOn { get; set; }
        public int AccountTypeInteger { get; internal set; }
        public string AlcoholStatus { get; set; }
        public string MarijuanaStatus { get; set; }
        //public Guid TargetProfileId { get; set; }
        //public string TargetProfileIdString { get; internal set; }
        public string IdPlainString { get; internal set; }
        public int AccountTypeNumber { get; internal set; }
        public int HisAgeTo { get; internal set; }
        public int HisAgeFrom { get; internal set; }
        public int HerAgeTo { get; internal set; }
        public int HerAgeFrom { get; internal set; }
        public int OnlyWithPhotos { get; internal set; }
        public string HisOrientation { get; internal set; }
        public string HerOrientation { get; internal set; }
        public string InterestedIn { get; internal set; }
        public string SwingStyleString { get; internal set; }
        public int TargetLikes { get; set; }
        public bool TargetLikesBool { get; internal set; }
        public bool TargetNotLikesBool { get; internal set; }
        public string TargetLikesIcon { get; internal set; }
        public bool ShowPartnerDetailsHtmlTable { get; internal set; }
        public bool ShowPartnerDetailsTitle { get; set; }
        public string PartnerBirthGender { get; internal set; }
        public int PartnerAgeInt { get; internal set; }
        public string AvailabilityButtonLabel { get; internal set; }
        public string Image1Url { get; set; }
        public string Image2Url { get; set; }
        public string Image3Url { get; set; }
        public string Image4Url { get; set; }
        public string Image5Url { get; set; }
        public string Image1Id { get; set; }
        public string Image2Id { get; set; }
        public string Image3Id { get; set; }
        public string Image4Id { get; set; }
        public string Image5Id { get; set; }
        public string PrivateImage1Url { get; set; }
        public string PrivateImage2Url { get; set; }
        public string PrivateImage3Url { get; set; }
        public string PrivateImage4Url { get; set; }
        public string PrivateImage5Url { get; set; }
        public List<MyImage> MyPrivateImagesList { get; set; }
        public string PrivateImage1Id { get; set; }
        public string PrivateImage2Id { get; set; }
        public string PrivateImage3Id { get; set; }
        public string PrivateImage4Id { get; set; }
        public string PrivateImage5Id { get; set; }
        public int PrivateImage { get; set; }
        public string GrantRevokePermissionLabel { get; internal set; }
        public string TaglineTruncated { get; set; }
    }
}