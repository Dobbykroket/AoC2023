using static AoC2023.tools.Utils;

namespace AoCTests;

public class UtilsTest
{
    [Theory]
    [InlineData(new []{2,3, 2,4, 1,5}, 26)]
    [InlineData(new []{6,11, 13,16, 9,21, 19,25}, 89469)]
    public void CRTTest(int[] inputs, int expected)
    {
        //Arrange
        (long, long)[] funcs = new (long, long)[inputs.Length / 2];
        for (int i = 0; i < inputs.Length / 2; i++)
        {
            funcs[i] = (inputs[2*i], inputs[2*i + 1]);
        }
        
        //Act+Assert
        Assert.Equal(expected, CRT(funcs));
    }
    
    [Theory]
    [InlineData(283, 307, 1)]
    [InlineData(44, 284, 4)]
    [InlineData(27, 999, 27)]
    [InlineData(0, 5, 0)]
    [InlineData(22, 22, 22)]
    public void GCD_TwoIntInput(int a, int b, int expected)
    {
        Assert.Equal(expected, GCD(a, b));
    }

    [Theory]
    [InlineData(new long[] {4, 8, 16}, 4)]
    [InlineData(new long[] {4, 8, 16, 17}, 1)]
    [InlineData(new long[] {3, 3, 3, 3, 3, 3}, 3)]
    [InlineData(new long[] {25, 88, 74, 0}, 0)]
    [InlineData(new long[] {25, 0, 74, 88}, 0)]
    public void GCD_ArrayInput(long[] arr, int expected)
    {
        Assert.Equal(expected, GCD(arr));
    }

    [Fact]
    public void GCD_ArrayInput_ArrayTooSmall_ThrowsException()
    {
        long[] arr = { 0 };
        
        Assert.Throws(typeof(ArgumentException), () =>
        {
            return GCD(arr);
        });
    }

    [Theory]
    [InlineData(2, 5, 10)]
    [InlineData(5, 5, 5)]
    public void LCM_TwoIntInput(int a, int b, int expected)
    {
        Assert.Equal(expected, LCM(a, b));
    }

    [Fact]
    public void LCM_Zero_ThrowsException()
    {
        Assert.Throws(typeof(ArgumentException), () =>
        {
            return LCM(0, 1);
        });
    }

    [Theory]
    [InlineData(new [] {2, 3, 5}, 30)]
    [InlineData(new [] {5, 5, 5}, 5)]
    public void LCM_ArrayInput(int[] arr, int expected)
    {
        Assert.Equal(expected, LCM(arr));
    }

    [Fact]
    public void LCM_ArrayInput_ArrayTooSmall_ThrowsException()
    {
        int[] arr = { 0 };
        
        Assert.Throws(typeof(ArgumentException), () =>
        {
            return LCM(arr);
        });
    }

    [Fact]
    public void LCM_ArrayInput_ArrayContainsZero_ThrowsException()
    {
        int[] arr = { 0, 1, 2 };
        
        Assert.Throws(typeof(ArgumentException), () =>
        {
            return LCM(arr);
        });
    }
    
    
}