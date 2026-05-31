using KooliProjekt.WindowsForms.Api;
using Moq;
using Xunit;

namespace KooliProjekt.WindowsForms.UnitTests
{
    public class MainViewPresenterTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IMainView> _mainViewMock;
        private readonly MainViewPresenter _presenter;

        public MainViewPresenterTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _mainViewMock = new Mock<IMainView>();
            _presenter = new MainViewPresenter(_apiClientMock.Object, _mainViewMock.Object);
        }

        [Fact]
        public async Task LoadData_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult<PagedResult<Toiduaine>>();
            faultyResponse.AddError("An error occurred while fetching data.");

            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = null)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task LoadData_should_set_DataSource_with_valid_response()
        {
            // Arrange
            var validResponse = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>
                    {
                        new Toiduaine { Id = 1, Nimetus = "Leib" },
                        new Toiduaine { Id = 2, Nimetus = "Kana" }
                    }
                }
            };

            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(validResponse)
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = validResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_clear_fields_with_null_selection()
        {
            // Arrange
            var selected = (Toiduaine)null;

            _mainViewMock.SetupSet(view => view.CurrentId = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentNimetus = "").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentEnergia = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentValgud = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentSusivesikud = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentMillestSuhkrud = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentRasvad = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentMillestKullastunud = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentKiudained = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentSool = 0).Verifiable();

            // Act
            _presenter.SetSelection(selected);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_set_fields_with_valid_selection()
        {
            // Arrange
            var selected = new Toiduaine
            {
                Id = 1,
                Nimetus = "Leib",
                Energia = 250m,
                Valgud = 9m,
                Susivesikud = 45m,
                MillestSuhkrud = 3m,
                Rasvad = 3m,
                MillestKullastunud = 1m,
                Kiudained = 6m,
                Sool = 1m
            };

            _mainViewMock.SetupSet(view => view.CurrentId = 1).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentNimetus = "Leib").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentEnergia = 250m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentValgud = 9m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentSusivesikud = 45m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentMillestSuhkrud = 3m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentRasvad = 3m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentMillestKullastunud = 1m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentKiudained = 6m).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentSool = 1m).Verifiable();

            // Act
            _presenter.SetSelection(selected);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.AddError("An error occurred while saving data.");

            _apiClientMock
                .Setup(client => client.Save(It.IsAny<Toiduaine>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var validSaveResponse = new OperationResult();
            var validListResponse = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>()
                }
            };

            _apiClientMock
                .Setup(client => client.Save(It.IsAny<Toiduaine>()))
                .ReturnsAsync(validSaveResponse)
                .Verifiable();
            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(validListResponse)
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = validListResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_when_user_didnot_confirmed()
        {
            // Arrange
            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(false)
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _mainViewMock.VerifyAll();
            _apiClientMock.Verify(client => client.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Delete_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.AddError("An error occurred while deleting data.");

            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(true)
                .Verifiable();
            _apiClientMock
                .Setup(client => client.Delete(It.IsAny<int>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var validDeleteResponse = new OperationResult();
            var validListResponse = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>()
                }
            };

            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(true)
                .Verifiable();
            _apiClientMock
                .Setup(client => client.Delete(It.IsAny<int>()))
                .ReturnsAsync(validDeleteResponse)
                .Verifiable();
            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(validListResponse)
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = validListResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }
    }
}
