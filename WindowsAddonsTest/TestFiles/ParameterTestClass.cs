using System.Collections.Generic;

class ParameterType { }
public class ParameterTestClass
{
    void Build(ParameterType parameterName)
    {

    }

    void Build(int parameterName = 300)
    {

    }


    void Build(List<string> parameterName = null)
    {

    }
}