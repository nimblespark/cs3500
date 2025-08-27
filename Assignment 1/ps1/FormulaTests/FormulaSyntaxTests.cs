// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright (c) 2025 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> [Insert Your Name] </authors>
// <date> [Insert the Date] </date>

namespace CS3500.FormulaTests;

using System.Security.Cryptography;
using CS3500.Formula3; // Change this using statement to use different formula implementations.

/// <summary>
///   <para>
///     The following class shows the basics of how to use the MSTest framework,
///     including:
///   </para>
///   <list type="number">
///     <item> How to catch exceptions. </item>
///     <item> How a test of valid code should look. </item>
///   </list>
/// </summary>
[TestClass]
public class FormulaSyntaxTests
{
    // --- Tests for One Token Rule ---

    /// <summary>
    ///   <para>
    ///     This test makes sure the right kind of exception is thrown
    ///     when trying to create a formula with no tokens.
    ///   </para>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNoTokens_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula(string.Empty));
    }

    // --- Tests for Valid Token Rule ---

    /// <summary>
    ///   Formula with an invalid symbol should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestInvalidToken_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("$"));
    }

    /// <summary>
    ///   Formula with a variable and operators should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestVariableAndOperators_Valid()
    {
        _ = new Formula("x1+2");
    }

    // --- Tests for Closing Parenthesis Rule ---

    /// <summary>
    ///   Formula missing a closing parenthesis should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestMissingClosingParen_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("(1+2"));
    }

    /// <summary>
    ///   Formula with properly matched parentheses should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestMatchedParentheses_Valid()
    {
        _ = new Formula("(1+2)*(3+4)");
    }

    // --- Tests for Balanced Parentheses Rule ---

    /// <summary>
    ///   Too many closing parentheses should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestTooManyClosingParens_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("1+2)"));
    }

    /// <summary>
    ///   Nested balanced parentheses should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNestedParens_Valid()
    {
        _ = new Formula("((1+2)*(3-4))/5");
    }

    // --- Tests for First Token Rule ---

    /// <summary>
    ///   Make sure a simple well formed formula is accepted by the constructor.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenNumber_Valid()
    {
        _ = new Formula("1+1");
    }

    /// <summary>
    ///   Formula starting with an operator should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenOperator_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("+2*3"));
    }

    /// <summary>
    ///   Formula starting with a variable should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenVariable_Valid()
    {
        _ = new Formula("x1+2");
    }

    /// <summary>
    ///   Formula starting with a parenthesis should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenParen_Valid()
    {
        _ = new Formula("(2+3)*4");
    }

    // --- Tests for Last Token Rule ---

    /// <summary>
    ///   Formula ending with an operator should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenOperator_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("1+2+"));
    }

    /// <summary>
    ///   Formula ending with a number should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenNumber_Valid()
    {
        _ = new Formula("1+2");
    }

    /// <summary>
    ///   Formula ending with a variable should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenVariable_Valid()
    {
        _ = new Formula("1+x3");
    }

    /// <summary>
    ///   Formula ending with a closing parenthesis should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenParen_Valid()
    {
        _ = new Formula("1+(2*3)");
    }

    // --- Tests for Parentheses/Operator Following Rule ---

    /// <summary>
    ///   Number followed directly by another number should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestTwoNumbersInARow_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("1 2"));
    }

    /// <summary>
    ///   Operator followed by a valid number should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOperatorFollowedByNumber_Valid()
    {
        _ = new Formula("1+2");
    }

    /// <summary>
    ///   Opening parenthesis followed by a number should be valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestParenFollowedByNumber_Valid()
    {
        _ = new Formula("(2+3)");
    }

    /// <summary>
    ///   Closing parenthesis followed by a number should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestClosingParenFollowedByNumber_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("(2+3)4"));
    }

    // --- Tests for Extra Following Rule ---

    /// <summary>
    ///   Variable followed directly by another variable should throw an exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestTwoVariablesInARow_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("x y"));
    }

    /// <summary>
    ///   Variable followed by operator is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestVariableFollowedByOperator_Valid()
    {
        _ = new Formula("a47+3");
    }

    /// <summary>
    ///   Number followed by variable without operator should throw exception.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNumberFollowedByVariable_Invalid()
    {
        Assert.ThrowsExactly<FormulaFormatException>(() => _ = new Formula("2x"));
    }

    /// <summary>
    ///   Complex but valid formula should be accepted.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestComplexValidFormula_Valid()
    {
        _ = new Formula("((x1+2)*(y3-4))/z5");
    }
}
