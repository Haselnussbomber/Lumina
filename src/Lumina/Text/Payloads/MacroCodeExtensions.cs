using System.Reflection;

namespace Lumina.Text.Payloads;

/// <summary>Extension methods for <see cref="MacroCode"/>.</summary>
public static class MacroCodeExtensions
{
    private static readonly string?[] EncodedNames;

    static MacroCodeExtensions()
    {
        EncodedNames = new string?[256];
        foreach( var v in typeof( MacroCode ).GetFields( BindingFlags.Static | BindingFlags.Public ) )
        {
            if( v.GetRawConstantValue() is not byte b )
                continue;
            if( v.GetCustomAttribute< MacroCodeDataAttribute >() is not { } mcda )
                continue;
            EncodedNames[ b ] = mcda.EncodedName ?? v.Name.ToLowerInvariant();
        }
    }

    /// <summary>Gets the encoded name for an macro code, if available.</summary>
    /// <param name="v">The macro code.</param>
    /// <returns>The native name of the macro code, or <c>null</c> if not available.</returns>
    public static string? GetEncodeName( this MacroCode v ) => EncodedNames[ (int) v ];
}