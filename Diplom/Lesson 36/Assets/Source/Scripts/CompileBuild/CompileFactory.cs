using UnityEditor;
using UnityEngine;

public static class CompileFactory
{
    [MenuItem("AutoBuild/CompileForWindows")]
    private static void CompileForWindows()
    {
        ICompileFactory compileFactory = new SimpleWinCompileFactory();
        compileFactory.Compile();
    }

    [MenuItem("AutoBuild/CompileForAndroid")]
    private static void CompileForAndroid()
    {
        ICompileFactory compileFactory = new SimpleAndroidCompileFactory();
        compileFactory.Compile();
    }

    [MenuItem("AutoBuild/CompileAab")]
    private static void CompileForAab()
    {
        EditorUserBuildSettings.buildAppBundle = true;
        ICompileFactory compileFactory = new SimpleAabCompileFactory();
        compileFactory.Compile();
        EditorUserBuildSettings.buildAppBundle = false;
    }

    [MenuItem("AutoBuild/CompileAll")]
    private static void CompileAll()
    {
        CompileForWindows();
        CompileForAndroid();
        CompileForAab();
    }
}

public interface ICompileFactory
{
    public void Compile();
}

public class SimpleWinCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scence(), Application.persistentDataPath + "/GameWin.exe",
            BuildTarget.StandaloneWindows,
            BuildOptions.Development);
    }

    private EditorBuildSettingsScene[] Scence()
    {
        return EditorBuildSettings.scenes;
    }
}

public class SimpleAndroidCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scence(), Application.persistentDataPath + "/GameAndroid.apk", BuildTarget.Android,
            BuildOptions.None);
    }

    private EditorBuildSettingsScene[] Scence()
    {
        return EditorBuildSettings.scenes;
    }
}

public class SimpleAabCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scence(), Application.persistentDataPath + "/GameAab.aab", BuildTarget.Android,
            BuildOptions.None);
    }

    private EditorBuildSettingsScene[] Scence()
    {
        return EditorBuildSettings.scenes;
    }
}