using System.ComponentModel;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form, IMainView
    {
        private readonly IApiClient _apiClient;
        private MainViewPresenter _mainViewPresenter;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<Toiduaine> DataSource
        {
            get { return (IList<Toiduaine>)dataGridView1.DataSource; }
            set { dataGridView1.DataSource = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Toiduaine SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentId
        {
            get { return int.Parse(idField.Text); }
            set { idField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurrentNimetus
        {
            get { return nimetusField.Text; }
            set { nimetusField.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentEnergia
        {
            get { return decimal.Parse(energiaField.Text); }
            set { energiaField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentValgud
        {
            get { return decimal.Parse(valgudField.Text); }
            set { valgudField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentSusivesikud
        {
            get { return decimal.Parse(susivesikudField.Text); }
            set { susivesikudField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentMillestSuhkrud
        {
            get { return decimal.Parse(millestSuhkrudField.Text); }
            set { millestSuhkrudField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentRasvad
        {
            get { return decimal.Parse(rasvadField.Text); }
            set { rasvadField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentMillestKullastunud
        {
            get { return decimal.Parse(millestKullastunudField.Text); }
            set { millestKullastunudField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentKiudained
        {
            get { return decimal.Parse(kiudainedField.Text); }
            set { kiudainedField.Text = value.ToString(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal CurrentSool
        {
            get { return decimal.Parse(soolField.Text); }
            set { soolField.Text = value.ToString(); }
        }

        public void SetPresenter(MainViewPresenter presenter)
        {
            _mainViewPresenter = presenter;
        }

        public Form1(IApiClient apiClient)
        {
            _apiClient = apiClient;

            InitializeComponent();

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            saveCommand.Click += SaveCommand_Click;
            addCommand.Click += AddCommand_Click;
            deleteCommand.Click += DeleteCommand_Click;
        }

        private async void DeleteCommand_Click(object sender, EventArgs e)
        {
            var message = "Oled kindel, et soovid kustutada " + nimetusField.Text + "?";
            var answer = MessageBox.Show(message, "Kustutamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
            {
                return;
            }

            var id = int.Parse(idField.Text);
            var result = await _apiClient.Delete(id);
            if (result.HasErrors)
            {
                ShowError("Viga kustutamisel", result);
            }

            await _mainViewPresenter.LoadData();
        }

        private void AddCommand_Click(object sender, EventArgs e)
        {
            idField.Text = "0";
            nimetusField.Text = "";
            energiaField.Text = "0";
            valgudField.Text = "0";
            susivesikudField.Text = "0";
            millestSuhkrudField.Text = "0";
            rasvadField.Text = "0";
            millestKullastunudField.Text = "0";
            kiudainedField.Text = "0";
            soolField.Text = "0";
        }

        private async void SaveCommand_Click(object sender, EventArgs e)
        {
            var toiduaine = new Toiduaine();
            toiduaine.Id = int.Parse(idField.Text);
            toiduaine.Nimetus = nimetusField.Text;
            toiduaine.Energia = decimal.Parse(energiaField.Text);
            toiduaine.Valgud = decimal.Parse(valgudField.Text);
            toiduaine.Susivesikud = decimal.Parse(susivesikudField.Text);
            toiduaine.MillestSuhkrud = decimal.Parse(millestSuhkrudField.Text);
            toiduaine.Rasvad = decimal.Parse(rasvadField.Text);
            toiduaine.MillestKullastunud = decimal.Parse(millestKullastunudField.Text);
            toiduaine.Kiudained = decimal.Parse(kiudainedField.Text);
            toiduaine.Sool = decimal.Parse(soolField.Text);

            var result = await _apiClient.Save(toiduaine);
            if (result.HasErrors)
            {
                ShowError("Viga salvestamisel", result);
            }

            await _mainViewPresenter.LoadData();
        }

        // Koosta etteantud veateatest ja OperationResult sees olevatest vigadest
        // veateade ja näita seda kasutajale
        public void ShowError(string message, OperationResult result)
        {
            var error = message + "\r\n";
            var apiErrors = "";
            var propertyErrors = "";

            if (result.Errors != null)
            {
                foreach (var apiError in result.Errors)
                {
                    apiErrors += apiError + "\r\n";
                }
            }

            if (result.PropertyErrors != null)
            {
                foreach (var propertyError in result.PropertyErrors)
                {
                    propertyErrors += propertyError.Key + ": " + propertyError.Value;
                }
            }

            if (!string.IsNullOrEmpty(apiErrors))
            {
                error += "\r\n" + apiErrors + "\r\n";
            }

            if (!string.IsNullOrEmpty(propertyErrors))
            {
                error += "\r\n" + propertyErrors;
            }

            error = error.Trim();

            MessageBox.Show(error, "Viga!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                _mainViewPresenter.SetSelection(null);
                return;
            }

            var selected = (Toiduaine)dataGridView1.CurrentRow.DataBoundItem;
            _mainViewPresenter.SetSelection(selected);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await _mainViewPresenter.LoadData();
        }
    }
}
