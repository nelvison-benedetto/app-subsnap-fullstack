using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SubSnap.Infrastructure")]

//il nome esatto lo trovi .Infrastructure in 
//< PropertyGroup >
//  < AssemblyName > SubSnap.Infrastructure </ AssemblyName >
//</ PropertyGroup >

//!!!questo lo faccio xk in .Infrastructure\Repositories\Implementations\UserRepository.cs voglio usare SetId() di .Core\Domain\Entities\User.cs (che pero il methodo è 'internal'!!!)

//questa tecnica è anche usata da Microsoft, tiene i confini forti, ma con qualche 'puntura' davvero mirata dove vuoi.
