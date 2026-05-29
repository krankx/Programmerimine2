using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public class MainViewPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IMainView _mainView;

        private Toiduaine _selected;

        public MainViewPresenter(IApiClient apiClient, IMainView mainView)
        {
            _apiClient = apiClient;
            _mainView = mainView;
            _mainView.SetPresenter(this);
        }

        public async Task LoadData()
        {
            var response = await _apiClient.List(1, 100);
            if (response.HasErrors)
            {
                _mainView.ShowError("Viga andmete laadimisel", response);
                _mainView.DataSource = null;
                return;
            }

            _mainView.DataSource = response.Value.Results;
        }

        public void SetSelection(Toiduaine selected)
        {
            _selected = selected;
            if (_selected == null)
            {
                _mainView.CurrentId = 0;
                _mainView.CurrentNimetus = "";
                _mainView.CurrentEnergia = 0;
                _mainView.CurrentValgud = 0;
                _mainView.CurrentSusivesikud = 0;
                _mainView.CurrentMillestSuhkrud = 0;
                _mainView.CurrentRasvad = 0;
                _mainView.CurrentMillestKullastunud = 0;
                _mainView.CurrentKiudained = 0;
                _mainView.CurrentSool = 0;
            }
            else
            {
                _mainView.CurrentId = _selected.Id;
                _mainView.CurrentNimetus = _selected.Nimetus;
                _mainView.CurrentEnergia = _selected.Energia;
                _mainView.CurrentValgud = _selected.Valgud;
                _mainView.CurrentSusivesikud = _selected.Susivesikud;
                _mainView.CurrentMillestSuhkrud = _selected.MillestSuhkrud;
                _mainView.CurrentRasvad = _selected.Rasvad;
                _mainView.CurrentMillestKullastunud = _selected.MillestKullastunud;
                _mainView.CurrentKiudained = _selected.Kiudained;
                _mainView.CurrentSool = _selected.Sool;
            }
        }

        public async Task Save()
        {
            var toiduaine = new Toiduaine();
            toiduaine.Id = _mainView.CurrentId;
            toiduaine.Nimetus = _mainView.CurrentNimetus;
            toiduaine.Energia = _mainView.CurrentEnergia;
            toiduaine.Valgud = _mainView.CurrentValgud;
            toiduaine.Susivesikud = _mainView.CurrentSusivesikud;
            toiduaine.MillestSuhkrud = _mainView.CurrentMillestSuhkrud;
            toiduaine.Rasvad = _mainView.CurrentRasvad;
            toiduaine.MillestKullastunud = _mainView.CurrentMillestKullastunud;
            toiduaine.Kiudained = _mainView.CurrentKiudained;
            toiduaine.Sool = _mainView.CurrentSool;

            var result = await _apiClient.Save(toiduaine);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga salvestamisel", result);
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if (!_mainView.ConfirmDelete())
            {
                return;
            }

            var result = await _apiClient.Delete(_mainView.CurrentId);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga kustutamisel", result);
                return;
            }

            await LoadData();
        }
    }
}
