namespace OpenStore.Omnichannel.Panel.Components.DataGrid
{
    internal class DataGridEnvelope<TItem>
    {
        public DataGridEnvelope(TItem item)
        {
            Item = item;
        }

        public TItem Item { get; }

        public bool Selected { get; set; }
    }
}