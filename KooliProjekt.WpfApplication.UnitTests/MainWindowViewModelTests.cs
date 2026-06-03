using Moq;

namespace KooliProjekt.WpfApplication.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IDialogProvider> _dialogProviderMock;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _dialogProviderMock = new Mock<IDialogProvider>();
            _viewModel = new MainWindowViewModel(_apiClientMock.Object, _dialogProviderMock.Object);
        }

        [Fact]
        public void SelectedItem_should_return_correct_item()
        {
            // Arrange
            var item = new Toiduaine { Id = 1, Nimetus = "Test" };

            // Act
            _viewModel.SelectedItem = item;

            // Assert
            Assert.Equal(item, _viewModel.SelectedItem);
        }

        [Fact]
        public void SelectedItem_should_call_notify_property_changed()
        {
            // Arrange
            var item = new Toiduaine { Id = 1, Nimetus = "Test" };
            var propertyChangedRaised = false;
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(MainWindowViewModel.SelectedItem))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            _viewModel.SelectedItem = item;

            // Assert
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public async Task LoadData_should_load_data_from_api_client()
        {
            // Arrange
            var apiResult = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>
                    {
                        new Toiduaine { Id = 1, Nimetus = "Test 1" },
                        new Toiduaine { Id = 2, Nimetus = "Test 2" }
                    }
                }
            };

            _apiClientMock.Setup(client => client.List(1, 100))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act
            await _viewModel.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            Assert.Equal(2, _viewModel.Data.Count);
            Assert.Equal(1, _viewModel.Data[0].Id);
            Assert.Equal(2, _viewModel.Data[1].Id);
        }

        [Fact]
        public async Task LoadData_should_show_error_when_api_client_fails()
        {
            // Arrange
            var apiResult = new OperationResult<PagedResult<Toiduaine>>
            {
                Errors = new List<string> { "Error" }
            };

            _apiClientMock.Setup(client => client.List(1, 100))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act
            await _viewModel.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            Assert.Empty(_viewModel.Data);
        }

        [Fact]
        public void AddNew_Command_Should_Set_Empty_SelectedItem()
        {
            // Arrange

            // Act
            _viewModel.AddNewCommand.Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedItem);
            Assert.Equal(0, _viewModel.SelectedItem.Id);
        }

        [Fact]
        public void SaveCommand_should_load_data_if_no_errors()
        {
            // Arrange
            var loadDataApiResult = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>
                    {
                        new Toiduaine { Id = 1, Nimetus = "Test 1" },
                        new Toiduaine { Id = 2, Nimetus = "Test 2" }
                    }
                }
            };
            var saveDataApiResult = new OperationResult();
            var toiduaineToSave = new Toiduaine { Id = 1, Nimetus = "Test" };

            _apiClientMock.Setup(client => client.Save(It.IsAny<Toiduaine>()))
                .ReturnsAsync(saveDataApiResult)
                .Verifiable();
            _apiClientMock.Setup(client => client.List(1, 100))
                .ReturnsAsync(loadDataApiResult)
                .Verifiable();

            // Act
            _viewModel.SaveCommand.Execute(toiduaineToSave);

            // Assert
            _apiClientMock.VerifyAll();
        }

        [Fact]
        public async Task SaveCommand_should_return_when_api_gave_error()
        {
            // Arrange
            var saveDataApiResult = new OperationResult
            {
                Errors = new List<string> { "Save error" }
            };
            var toiduaineToSave = new Toiduaine { Id = 1, Nimetus = "Test" };

            _apiClientMock.Setup(client => client.Save(It.IsAny<Toiduaine>()))
                .ReturnsAsync(saveDataApiResult)
                .Verifiable();
            _dialogProviderMock.Setup(provider => provider.ShowError(It.IsAny<string>()))
                .Verifiable();

            // Act
            _viewModel.SaveCommand.Execute(toiduaineToSave);

            // Assert
            _apiClientMock.VerifyAll();
            _dialogProviderMock.VerifyAll();
            _apiClientMock.Verify(client => client.List(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task SaveCommand_can_execute_when_selected_item_is_not_null()
        {
            // Arrange & Act & Assert
            _viewModel.SelectedItem = null;
            Assert.False(_viewModel.SaveCommand.CanExecute(null));

            _viewModel.SelectedItem = new Toiduaine { Id = 1, Nimetus = "Test" };
            Assert.True(_viewModel.SaveCommand.CanExecute(null));
        }

        [Fact]
        public async Task DeleteCommand_should_return_when_no_confirmation()
        {
            // Arrange
            // DeleteCommand teeb return kui IDialogClient.Confirm() tagastab false
            var toiduaineToDelete = new Toiduaine { Id = 1, Nimetus = "Test" };

            _dialogProviderMock.Setup(provider => provider.Confirm(It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(toiduaineToDelete);

            // Assert
            _dialogProviderMock.VerifyAll();
            _apiClientMock.Verify(client => client.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCommand_should_load_data_if_no_errors()
        {
            // Arrange
            var deleteDataApiResult = new OperationResult();
            var loadDataApiResult = new OperationResult<PagedResult<Toiduaine>>
            {
                Value = new PagedResult<Toiduaine>
                {
                    Results = new List<Toiduaine>()
                }
            };
            var toiduaineToDelete = new Toiduaine { Id = 1, Nimetus = "Test" };

            _dialogProviderMock.Setup(provider => provider.Confirm(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();
            _apiClientMock.Setup(client => client.Delete(It.IsAny<int>()))
                .ReturnsAsync(deleteDataApiResult)
                .Verifiable();
            _apiClientMock.Setup(client => client.List(1, 100))
                .ReturnsAsync(loadDataApiResult)
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(toiduaineToDelete);

            // Assert
            _dialogProviderMock.VerifyAll();
            _apiClientMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteCommand_should_return_when_api_gave_error()
        {
            // Arrange
            var deleteDataApiResult = new OperationResult
            {
                Errors = new List<string> { "Delete error" }
            };
            var toiduaineToDelete = new Toiduaine { Id = 1, Nimetus = "Test" };

            _dialogProviderMock.Setup(provider => provider.Confirm(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();
            _apiClientMock.Setup(client => client.Delete(It.IsAny<int>()))
                .ReturnsAsync(deleteDataApiResult)
                .Verifiable();
            _dialogProviderMock.Setup(provider => provider.ShowError(It.IsAny<string>()))
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(toiduaineToDelete);

            // Assert
            _dialogProviderMock.VerifyAll();
            _apiClientMock.VerifyAll();
            _apiClientMock.Verify(client => client.List(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCommand_can_execute_when_selected_item_is_not_null_and_id_is_not_zero()
        {
            // Arrange & Act & Assert
            _viewModel.SelectedItem = null;
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));

            _viewModel.SelectedItem = new Toiduaine { Id = 0, Nimetus = "Test" };
            Assert.False(_viewModel.DeleteCommand.CanExecute(null));

            _viewModel.SelectedItem = new Toiduaine { Id = 1, Nimetus = "Test" };
            Assert.True(_viewModel.DeleteCommand.CanExecute(null));
        }
    }
}
