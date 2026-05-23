using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
        private readonly IApiClient _apiClient;

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

            await LoadToiduained();
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
            await LoadToiduained();
        }

        // Koosta etteantud veateatest ja OperationResult sees olevatest vigadest
        // veateade ja näita seda kasutajale
        private void ShowError(string message, OperationResult result)
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
                return;
            }

            var selected = (Toiduaine)dataGridView1.CurrentRow.DataBoundItem;
            if (selected == null)
            {
                return;
            }

            idField.Text = selected.Id.ToString();
            nimetusField.Text = selected.Nimetus;
            energiaField.Text = selected.Energia.ToString();
            valgudField.Text = selected.Valgud.ToString();
            susivesikudField.Text = selected.Susivesikud.ToString();
            millestSuhkrudField.Text = selected.MillestSuhkrud.ToString();
            rasvadField.Text = selected.Rasvad.ToString();
            millestKullastunudField.Text = selected.MillestKullastunud.ToString();
            kiudainedField.Text = selected.Kiudained.ToString();
            soolField.Text = selected.Sool.ToString();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadToiduained();
        }

        private async Task LoadToiduained()
        {
            var response = await _apiClient.List(1, 100);
            if (response.HasErrors)
            {
                ShowError("Viga andmete laadimisel", response);
                dataGridView1.DataSource = null;
                return;
            }

            dataGridView1.DataSource = response.Value.Results;
        }
    }
}
