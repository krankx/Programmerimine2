using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var url = "http://localhost:5086/api/Toiduained/List";
            url += "?page=1&pageSize=10";

            using var client = new HttpClient();
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<Toiduaine>>>(url).Result;
            dataGridView1.DataSource = response.Value.Results;
        }
    }
}
