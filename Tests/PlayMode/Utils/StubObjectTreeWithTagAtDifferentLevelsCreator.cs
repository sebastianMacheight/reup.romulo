using ReupVirtualTwin.dataModels;
using UnityEngine;

public class StubObjectTreeWithTagAtDifferentLevelsCreator
{
    public static string parentId = "parent-id";
    public static string child0Id = "child0-id";
    public static string child1Id = "child1-id";
    public static string child2Id = "child1-id";
    public static string grandChild00Id = "grandChild00-id";
    public static string grandChild01Id = "grandChild01-id";

    public static Tag tagAtDifferentLevels = new Tag() { id = "tag-at-different-levels" };

    /// <summary>
    /// Creates a mock object tree with an object with a tag at the second level
    /// and another object with the same tag at the third level in a different branch.
    /// The returned structure is the following (Objects with an X have the mentioned tag):
    ///                                        P
    ///                                     /  |  \
    ///                                   C0  C1X  C2
    ///                                  /  \
    ///                               GC00X GC01
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateMockObjectWithArbitraryTagAtSecondAndThirdLevel()
    {
        GameObject parent = new(parentId);
        StubObjectCreatorUtils.AssignIdToObject(parent, parentId);

        GameObject child0 = new(child0Id);
        child0.transform.parent = parent.transform;
        StubObjectCreatorUtils.AssignIdToObject(child0, child0Id);

        GameObject grandChild00 = new(grandChild00Id);
        grandChild00.transform.parent = child0.transform;
        StubObjectCreatorUtils.AssignIdToObject(grandChild00, grandChild00Id);
        StubObjectCreatorUtils.AssignTagsToObject(grandChild00, new Tag[] {tagAtDifferentLevels});

        GameObject grandChild01 = new(grandChild01Id);
        grandChild01.transform.parent = child0.transform;
        StubObjectCreatorUtils.AssignIdToObject(grandChild01, grandChild01Id);

        GameObject child1 = new(child1Id);
        child1.transform.parent = parent.transform;
        StubObjectCreatorUtils.AssignIdToObject(child1, child1Id);
        StubObjectCreatorUtils.AssignTagsToObject(child1, new Tag[] {tagAtDifferentLevels});

        GameObject child2 = new(child2Id);
        child2.transform.parent = parent.transform;
        StubObjectCreatorUtils.AssignIdToObject(child2, child2Id);

        return parent;
    }

}
