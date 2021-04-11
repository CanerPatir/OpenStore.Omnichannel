namespace OpenStore.Omnichannel.Infrastructure
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Content { get; set; }
        public string Preview { get; set; }
        public string ActionLink { get; set; }
        public string ActionText { get; set; }
    }
}