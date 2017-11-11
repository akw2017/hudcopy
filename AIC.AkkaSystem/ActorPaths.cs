namespace AIC.AkkaSystem
{
    /// <summary>
    /// Static helper class used to define paths to fixed-name actors
    /// (helps eliminate errors when using <see cref="ActorSelection"/>)
    /// </summary>
    public static class ActorPaths
    {
        public static readonly ActorMetaData SignalCoordinatorActor = new ActorMetaData("signalCoordinatorActor", "akka://AICActorSystem/user/signalCoordinatorActor");
        public static readonly ActorMetaData SignalCacheActor = new ActorMetaData("signalCacheActor", "akka://AICActorSystem/user/signalCoordinatorActor/signalCacheActor");
        public static readonly ActorMetaData TreesActor = new ActorMetaData("treesActor", "akka://AICActorSystem/user/signalCoordinatorActor/treesActor");
        public static readonly ActorMetaData ObjectsViewerActor = new ActorMetaData("objectsViewerActor", "akka://AICActorSystem/user/objectsViewerActor");
    }

    /// <summary>
    /// Meta-data class
    /// </summary>
    public class ActorMetaData
    {
        public ActorMetaData(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }

        public string Path { get; private set; }
    }
}
