namespace DemoApi.Application.ViewModel;

public record StateVm(long Id,
	string Name,
	long CountryId,
	CountryVm Country
	);

