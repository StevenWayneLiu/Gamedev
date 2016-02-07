using UnityEngine;
using System.Collections;

public class SkillDatabase : Database<Skill> {

	public SkillDatabase()
        : base()
    {

    }
    public SkillDatabase(SkillDatabase orig)
        : base(orig)
    {

    }

    public override Skill CopyEntry(int index)
    {
        return new Skill(entries[index]);
    }
}
