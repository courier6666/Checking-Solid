// See https://aka.ms/new-console-template for more information
using SOLIDCheckingLibrary;

IDictionary<List<List<int>>, Stack<int>> v = null;

ExternalMethod(new B());

InterfaceSegregation.ClassFollowsPrinciple(typeof(A));

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

