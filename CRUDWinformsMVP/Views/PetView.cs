namespace CRUDWinFormsMVP.Views
{
    public partial class PetView : Form, IPetView
    {
        public PetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            // Temporarily
            tabControl1.TabPages.Remove(tabPagePetDetail);
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
                // Others
            };
        }

        public string PetID { get; set; }
        public string PetName { get => txtPetName.Text; set => txtPetName.Text = value; }
        public string PetType { get => txtPetType.Text; set => txtPetType.Text = value; }
        public string PetColor { get => txtPetColor.Text; set => txtPetColor.Text = value; }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsEdited { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler AddEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler EditEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetPetListBindingSource(BindingSource bindingSource)
        {
            dataGridView.DataSource = bindingSource;
        }
    }
}