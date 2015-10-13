﻿public class Webservice
{

			public class Register 
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oRegister";

				public const string EmailAddress="EmailAddress";
				public const string FullName="FullName";
				public const string Password="Password";
				public const string Sex="Sex";
				public const string Devicetype="DeviceType";
				public const string DeviceToken="DeviceToken";
			}

			public class Login
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oLogin";

				public const string EmailAddress="EmailAddress";
				public const string Password="Password";
				public const string Devicetype="DeviceType";
				public const string DeviceToken="DeviceToken";
			}


			public class Logout
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oLogout";
				
				public const string SessionGUID="SessionGUID";
			}





			public class GetAvatarDetails
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oGetAvatarDetails";

				public const string SessionGUID="SessionGUID";
			}

			public class SetAvatarDetails
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oSaveAvatarDetails";
				
				public const string SessionGUID="SessionGUID";
				public const string Sex="Sex";
				public const string Height="Height";
				public const string Weight="Weight";
				public const string AvatartName="AvatartName";
				public const string AvatarImage="AvatarImage";
			}

			public class SaveSearchItem
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oSaveSearchItem";
				
				public const string SessionGUID="SessionGUID";
				public const string ImageData="ImageData";
			}

			public class GetItemsList
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oGetSearchItems";
				
				public const string SessionGUID="SessionGUID";
			}

			public class Fbtwitter
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oSignInFacebookTwitterUser";

				public const string EmailAddress="EmailAddress";
				public const string FullName="FullName";
				public const string Type="Type";
				public const string UniqueID="UniqueID";
				public const string Devicetype="DeviceType";
				public const string DeviceToken="DeviceToken";
			}

			public class oGetStores 
			{
				public const string url="http://ec2-52-11-139-161.us-west-2.compute.amazonaws.com/Screenshopservice/Screenshop.svc/oGetStores";
				public const string imageUrl="imageUrl";
				public const string latitude="latitude";
				public const string longitude="longitude";
			}





}


