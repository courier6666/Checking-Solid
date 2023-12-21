// See https://aka.ms/new-console-template for more information
using SOLIDCheckingLibrary;


Console.WriteLine("Hello, World!");
OpenExtClosedMod.IsClassExtensible(typeof(A), ignoreInheritedClasses: false);

D c = new A();

c.B_(1);

class C
{
    public virtual void B_(int a)
    {
        Console.WriteLine("B");
    }
}
interface IB
{
    void B_interface();
}
abstract class D : C, IA, IB
{
    private int d_var = 1;
    public int Biba { get; set; }
    public virtual int Ciba { get; set; }
    public abstract int Diba {get;set;} 
    public void A_interface()
    {
        Console.WriteLine("D");
    }

    public override void B_(int a)
    {
        Console.WriteLine("B");
    }

    public void B_interface()
    {
        throw new NotImplementedException();
    }
}
interface IA
{
    void A_interface();
    int Biba { get; set; }
}
class A : D
{
    private int i;
    private List<int> list;
    public void A_interface()
    {
        Console.WriteLine("A");
    }
    public A(IDictionary<List<int>,int> dict)
    {

    }
    public A()
    {

    }
    public A(int i)
    {
        this.i = i;
    }
    public int Sum(A a, int b)
    {
        throw new NotImplementedException();
    }

    public int Ha
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }

    }

    public override int Diba { get; set; }
}




