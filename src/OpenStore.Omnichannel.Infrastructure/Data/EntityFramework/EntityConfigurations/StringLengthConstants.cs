namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations
{
    internal static class StringLengthConstants
    {
        public const int DefaultStringLength = 255;
        public const int MediumStringLength = 512;
        public const int PhoneLength = 15;
        public const int TcknLength = 11;
        public const int EmailLength = 255;
        public const int _191_ = 191; // it is for mySql index creation specified key was too long errors 
    }
}