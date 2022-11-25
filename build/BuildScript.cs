using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlubuCore.Context;
using FlubuCore.Context.Attributes.BuildProperties;
using FlubuCore.Context.FluentInterface.Interfaces;
using FlubuCore.IO;
using FlubuCore.Scripting;

namespace Build {
    /// <summary>
    /// �����ű�
    /// </summary>
    public class BuildScript : DefaultBuildScript {
        /// <summary>
        /// ��������ļ���
        /// </summary>
        [SolutionFileName]
        public string SolutionFileName { get; set; } = "../DCSoft.Util.sln";
        /// <summary>
        /// ��������
        /// </summary>
        [FromArg( "c|configuration" )]
        [BuildConfiguration]
        public string BuildConfiguration { get; set; } = "Release";
        /// <summary>
        /// Nuget���͵�ַ
        /// </summary>
        [FromArg( "nugetUrl" )]
        public string NugetUrl { get; set; } = "https://nuget.designcoding.com/v3/index.json";//"https://api.nuget.org/v3/index.json";
        /// <summary>
        /// Nuget��Կ
        /// </summary>
        [FromArg("nugetKey", "Nuget api key for publishing nuget packages.")]
        public string NugetApiKey { get; set; } = "w9AiPxG2otJzL7srbzFF8hETquU1tFuTKse85C3cP0CAkB";
        /// <summary>
        /// Դ����Ŀ¼
        /// </summary>
        public FullPath SourceDir => RootDirectory.CombineWith( "../src" );
        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        public FullPath TestDir => RootDirectory.CombineWith( "../test" );
        /// <summary>
        /// ���Ŀ¼
        /// </summary>
        public FullPath OutputDir => RootDirectory.CombineWith( "../output" );
        /// <summary>
        /// ��Ŀ�ļ��б�
        /// </summary>
        public List<FileFullPath> Projects { get; set; }
        /// <summary>
        /// ��Ԫ������Ŀ�ļ��б�
        /// </summary>
        public List<FileFullPath> UnitTestProjects { get; set; }
        /// <summary>
        /// ���ɲ�����Ŀ�ļ��б�
        /// </summary>
        public List<FileFullPath> IntegrationTestProjects { get; set; }
        /// <summary>
        /// ���Բ�����Ŀ�ļ��б�
        /// </summary>
        public List<FileFullPath> IgnoreTestProjects { get; set; }

        /// <summary>
        /// ��ȡ���ɲ�����Ŀ�ļ��б�
        /// </summary>
        protected List<FileFullPath> GetIntegrationTestProjects() {
            return IntegrationTestProjects.Where( t => IgnoreTestProjects.Exists( p => p.FileName == t.FileName ) == false ).ToList();
        }

        /// <summary>
        /// ����ǰ����
        /// </summary>
        /// <param name="context">��������������</param>
        protected override void BeforeBuildExecution( ITaskContext context ) {
            Projects = context.GetFiles( SourceDir, "*/*.csproj" );
            UnitTestProjects = context.GetFiles( TestDir, "*/*.Tests.csproj" );
            IntegrationTestProjects = context.GetFiles( TestDir, "*/*.Tests.Integration.csproj" );
            IgnoreTestProjects = new List<FileFullPath>();
            AddIgnoreTestProjects( context );
        }

        /// <summary>
        /// ��Ӻ��Բ�����Ŀ�ļ��б�
        /// </summary>
        private void AddIgnoreTestProjects( ITaskContext context ) {
            IgnoreTestProjects.AddRange( context.GetFiles( TestDir, "*/*.Oracle.Tests.Integration.csproj" ) );
        }

        /// <summary>
        /// ���ù���Ŀ��
        /// </summary>
        /// <param name="context">��������������</param>
        protected override void ConfigureTargets( ITaskContext context ) {
            var clean = Clean( context );
            var restore = Restore( context, clean );
            var build = Build( context, restore );
            var test = Test( context );
            var pack = Pack( context, clean );
            PublishNuGetPackage( context, pack );
        }

        /// <summary>
        /// ����������
        /// </summary>
        private ITarget Clean( ITaskContext context ) {
            return context.CreateTarget( "clean" )
                .SetDescription( "Clean the solution." )
                .AddCoreTask( t => t.Clean().AddDirectoryToClean( OutputDir, false ) );
        }

        /// <summary>
        /// ��ԭ��
        /// </summary>
        private ITarget Restore( ITaskContext context, params ITarget[] dependTargets ) {
            return context.CreateTarget( "restore" )
                .SetDescription( "Restore the solution." )
                .DependsOn( dependTargets )
                .AddCoreTask( t => t.Restore() );
        }

        /// <summary>
        /// ����������
        /// </summary>
        private ITarget Build( ITaskContext context, params ITarget[] dependTargets ) {
            return context.CreateTarget( "compile" )
                .SetDescription( "Compiles the solution." )
                .DependsOn( dependTargets )
                .AddCoreTask( t => t.Build() );
        }

        /// <summary>
        /// ���в���
        /// </summary>
        private ITarget Test( ITaskContext context, params ITarget[] dependTargets ) {
            var unitTest = UnitTest( context, dependTargets );
            var integrationTest = IntegrationTest( context, dependTargets );
            return context.CreateTarget( "test" )
                .SetDescription( "Run all tests." )
                .DependsOn( unitTest, integrationTest );
        }

        /// <summary>
        /// ���е�Ԫ����
        /// </summary>
        private ITarget UnitTest( ITaskContext context, params ITarget[] dependTargets ) {
            return context.CreateTarget( "unit.test" )
                .SetDescription( "Run unit tests." )
                .DependsOn( dependTargets )
                .ForEach( UnitTestProjects, ( project, target ) => {
                    target.AddCoreTask( t => t.Test().Project( project ) );
                } );
        }

        /// <summary>
        /// ���м��ɲ���
        /// </summary>
        private ITarget IntegrationTest( ITaskContext context, params ITarget[] dependTargets ) {
            return context.CreateTarget( "integration.test" )
                .SetDescription( "Run integration tests." )
                .DependsOn( dependTargets )
                .ForEach( GetIntegrationTestProjects(), ( project, target ) => {
                    target.AddCoreTask( t => t.Test().Project( project ) );
                } );
        }

        /// <summary>
        /// ����nuget��
        /// </summary>
        private ITarget Pack( ITaskContext context, params ITarget[] dependTargets ) {
            return context.CreateTarget( "pack" )
                .SetDescription( "Create nuget packages." )
                .DependsOn( dependTargets )
                .ForEach( Projects, ( project, target ) => {
                    target.AddCoreTask( t => t.Pack()
                        .Project( project )
                        .IncludeSymbols()
                        .OutputDirectory( OutputDir ) );
                } );
        }

        /// <summary>
        /// ����nuget��
        /// </summary>
        private void PublishNuGetPackage( ITaskContext context, params ITarget[] dependTargets ) {
            context.CreateTarget( "nuget.publish" )
                .SetDescription( "Publishes nuget packages" )
                .DependsOn( dependTargets )
                .Do( t => {
                    var packages = Directory.GetFiles( OutputDir, "*.nupkg" );
                    foreach ( var package in packages ) {
                        if( package.EndsWith( "symbols.nupkg", StringComparison.OrdinalIgnoreCase ) )
                            continue;
                        context.CoreTasks().NugetPush( package )
                            .DoNotFailOnError( ex => { Console.WriteLine( $"Failed to publish {package}.exception: {ex.Message}" ); } )
                            .ServerUrl( NugetUrl )
                            .ApiKey( NugetApiKey )
                            .Execute( context );
                    }
                } );
        }
    }
}