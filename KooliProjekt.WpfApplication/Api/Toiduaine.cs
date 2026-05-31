namespace KooliProjekt.WpfApplication
{
    public class Toiduaine : NotifyPropertyChangedBase
    {
        private int _id;
        private string _nimetus;
        private decimal _energia;
        private decimal _valgud;
        private decimal _susivesikud;
        private decimal _millestSuhkrud;
        private decimal _rasvad;
        private decimal _millestKullastunud;
        private decimal _kiudained;
        private decimal _sool;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        public string Nimetus
        {
            get
            {
                return _nimetus;
            }
            set
            {
                _nimetus = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Energia
        {
            get
            {
                return _energia;
            }
            set
            {
                _energia = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Valgud
        {
            get
            {
                return _valgud;
            }
            set
            {
                _valgud = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Susivesikud
        {
            get
            {
                return _susivesikud;
            }
            set
            {
                _susivesikud = value;
                NotifyPropertyChanged();
            }
        }

        public decimal MillestSuhkrud
        {
            get
            {
                return _millestSuhkrud;
            }
            set
            {
                _millestSuhkrud = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Rasvad
        {
            get
            {
                return _rasvad;
            }
            set
            {
                _rasvad = value;
                NotifyPropertyChanged();
            }
        }

        public decimal MillestKullastunud
        {
            get
            {
                return _millestKullastunud;
            }
            set
            {
                _millestKullastunud = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Kiudained
        {
            get
            {
                return _kiudained;
            }
            set
            {
                _kiudained = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Sool
        {
            get
            {
                return _sool;
            }
            set
            {
                _sool = value;
                NotifyPropertyChanged();
            }
        }
    }
}
