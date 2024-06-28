using Air.Domain.Entities;
using Air.Domain.Models;

namespace BlazorApp.Services
{
	public class ApiProductService(HttpClient Http) : IProductService<Airplane>
	{
		List<Airplane> _airplane;
		int _currentPage = 1;
		int _totalPages = 1;
		public IEnumerable<Airplane> Products => _airplane;
		public int CurrentPage => _currentPage;
		public int TotalPages => _totalPages;
		public event Action ListChanged;
		public async Task GetProducts(int pageNo, int pageSize)
		{
			// Url сервиса API
			var uri = Http.BaseAddress.AbsoluteUri;
			// данные для Query запроса
			var queryData = new Dictionary<string, string>
	   {
		  { "pageNo", pageNo.ToString() },
		 {"pageSize", pageSize.ToString() }
};
			var query = QueryString.Create(queryData);
			// Отправить запрос http
			var result = await Http.GetAsync(uri + query.Value);
			// В случае успешного ответа
			if (result.IsSuccessStatusCode)
			{
				// получить данные из ответа
				var responseData = await result.Content
				.ReadFromJsonAsync<ResponseData<ListModel<Airplane>>>();
				// обновить параметры
				_currentPage = responseData.Data.CurrentPage;
				_totalPages = responseData.Data.TotalPages;
				_airplane = responseData.Data.Items;
				ListChanged?.Invoke();
			}
			// В случае ошибки
			else
			{
				_airplane = null;
				_currentPage = 1;
				_totalPages = 1;
			}
		}
	}
}
