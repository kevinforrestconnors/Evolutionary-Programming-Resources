using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public abstract class Individual {

    public string name = "???";

    public int[] genome { get; set; }

    public virtual int fitness { get; set; }
    public virtual int resources { get; set; }
    public virtual int resourcesToMate { get; set; }
    public virtual int maxResources { get; set; }

    public Individual(int resources, int resourcesToMate, int maxResources)
    {
        this.resources = resources;
        this.resourcesToMate = resourcesToMate;
        this.maxResources = maxResources;
    }

    // Asexual reproduction
    public abstract Individual Reproduce();

    // Sexual reproduction
    public abstract Individual Reproduce(Individual other);

    public abstract bool CanMate();
}
