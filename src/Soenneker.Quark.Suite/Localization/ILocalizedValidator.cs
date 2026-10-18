namespace Soenneker.Quark;
/// <summary>Decorates an existing validator with a shared localized error key. Successful results and member names are preserved; messages follow the current culture on each validation.</summary>
public interface ILocalizedValidator : IQuarkValidator { }
