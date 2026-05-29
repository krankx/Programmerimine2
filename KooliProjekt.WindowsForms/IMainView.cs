namespace KooliProjekt.WindowsForms
{
    public interface IMainView
    {
        IList<Toiduaine> DataSource { get; set; }
        Toiduaine SelectedItem { get; set; }
        void SetPresenter(MainViewPresenter presenter);
        void ShowError(string message, OperationResult result);
        bool ConfirmDelete();

        int CurrentId { get; set; }
        string CurrentNimetus { get; set; }
        decimal CurrentEnergia { get; set; }
        decimal CurrentValgud { get; set; }
        decimal CurrentSusivesikud { get; set; }
        decimal CurrentMillestSuhkrud { get; set; }
        decimal CurrentRasvad { get; set; }
        decimal CurrentMillestKullastunud { get; set; }
        decimal CurrentKiudained { get; set; }
        decimal CurrentSool { get; set; }
    }
}
