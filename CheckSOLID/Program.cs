// See https://aka.ms/new-console-template for more information
using SOLIDCheckingLibrary;

IDictionary<List<List<int>>, Stack<int>> v = null;

DependencyInversion.CheckingSettings flags = DependencyInversion.CheckingSettings.IgnoreInheritedFields
   | DependencyInversion.CheckingSettings.IgnoreModels
   | DependencyInversion.CheckingSettings.IgnoreBasicCollectionTypes;

Console.WriteLine(flags);

void ExternalMethod(A instance)
{
    instance.Method();
}
class A
{
   public virtual void Method()
   {
        Console.WriteLine("A");
   }
   public int Pr
   {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
   }
}

class B : A
{
    public void Method()
    {
        Console.WriteLine("B");
    }
}

