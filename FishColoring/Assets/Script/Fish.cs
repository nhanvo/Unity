//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;
using gcs.FishColoring.ConstantVariable;

namespace gcs.FishColoring.FishObject
{
	public class Entity {
		protected string newPath;
		protected string oldPath;
		protected string exportPath;
		protected string name;
		protected Rect imageRectBox;

		public Entity() {
			newPath = "";
			oldPath = "";
			name = "";
			imageRectBox = new Rect();
			exportPath = "";
		} 

		public Entity(string newPath, string oldPath, string exportPath, string name, Rect imgRect) {
			this.newPath = newPath;
			this.oldPath = oldPath;
			this.exportPath = exportPath;
			this.name = name;
			this.imageRectBox = imgRect;
		}

		public void setNewPath(string path) {
			newPath = path;
		}

		public void setImageRectBox(Rect rect) {
			imageRectBox = rect;
		}

		public void setName(string name) {
			this.name = name;
		} 

		public void setOldPath(string path) {
			oldPath = path;
		}

		public void setExportPath(string path) {
			exportPath = path;
		}

		public string getNewPath() {
			return newPath;
		}

		public string getOldPath() {
			return oldPath;
		}

		public Rect getRect() {
			return imageRectBox;
		}

		public string getName() {
			return this.name;
		}

		public string getExportPath() {
			return exportPath;
		}


	}

	public class FishHead : Entity{
		public FishHead(Entity e)
		{
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class FishBody : Entity{
		public FishBody(Entity e) {
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class FishDosalFin : Entity {
		public FishDosalFin(Entity e) {
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class FishPelvicFin : Entity {
		public FishPelvicFin(Entity e) {
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class FishTail : Entity {
		public FishTail(Entity e) {
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class FishEye : Entity {
		public FishEye(Entity e) {
			this.newPath = e.getNewPath();
			this.oldPath = e.getOldPath();
			this.name = e.getName();
			this.imageRectBox = e.getRect();
			this.exportPath = e.getExportPath ();
		}
	}

	public class Fish
	{
		public enum EYE_COUNT {
			EYE_ONE = 0,
			EYE_TWO = 1,
			EYE_MAX
		};
		FishHead head;
		FishBody body;
		FishDosalFin dorsalFin;
		FishPelvicFin pelvicFin;
		FishTail tail;
		FishEye[] eye = new FishEye[(int)EYE_COUNT.EYE_MAX];		
		string persistentDataPath;
		string newFishFolderNumber;
		
		Entity[] fishData = {
			new Entity("NewFishPath", "OldFishPath/fish_head.png", "ExportFishPath", "fish_head", new Rect(19, 87, 329, 304)),
			new Entity("NewFishPath", "OldFishPath/fish_body.png", "ExportFishPath", "fish_body", new Rect(228, 86, 220, 304)),
			new Entity("NewFishPath", "OldFishPath/fish_tail.png", "ExportFishPath", "fish_tail", new Rect(448, 75, 170, 327)),
			new Entity("NewFishPath", "OldFishPath/fish_pelvic_fin.png", "ExportFishPath", "fish_pelvic_fin", new Rect(170, 45, 267, 92)),
			new Entity("NewFishPath", "OldFishPath/fish_dorsal_fin.png", "ExportFishPath", "fish_dorsal_fin", new Rect(157, 350, 279, 101)),
			new Entity("NewFishPath", "OldFishPath/fish_eye_one.png", "ExportFishPath", "fish_eye_one", new Rect(156, 254, 110, 87)),
			new Entity("NewFishPath", "OldFishPath/fish_eye_two.png", "ExportFishPath", "fish_eye_two", new Rect(45, 254, 104, 84))
		};

		public FishHead getHead() {
			return head;
		}

		public FishBody getBody() {
			return body;
		}

		public FishDosalFin getDorsalFin() {
			return dorsalFin;
		}

		public FishPelvicFin getPelvicFin() {
			return pelvicFin;
		}

		public FishTail getTail() {
			return tail;
		}

		public FishEye[] getEyes() {
			return eye;
		}

		public void SetPersistentDataPath(string per) {
			persistentDataPath = per;
		}

		public void SetNewFishFolderNumber(string newFolder) {
			newFishFolderNumber = newFolder;
		} 

		public Fish ()
		{

			foreach (Entity e in fishData) {
				if (e.getName().Equals(FishConstance.FISH_HEAD)) {
					head = new FishHead(e);

				} else if (e.getName().Equals(FishConstance.FISH_BODY)) {
					body = new FishBody(e);

				} else if (e.getName().Equals(FishConstance.FISH_TAIL)) {
					tail = new FishTail(e);

				} else if (e.getName().Equals(FishConstance.FISH_PELVIC_FIN)) {
					pelvicFin = new FishPelvicFin(e);

				} else if (e.getName().Equals(FishConstance.FISH_DORSAL_FIN)) {
					dorsalFin = new FishDosalFin(e);

				} else if (e.getName().Equals(FishConstance.FISH_EYE_ONE)) {
					eye[(int)EYE_COUNT.EYE_ONE] = new FishEye(e);

				} else if (e.getName().Equals(FishConstance.FISH_EYE_TWO)) {
					eye[(int)EYE_COUNT.EYE_TWO] = new FishEye(e);
				}
			}
		}

		public string getNewPath(Entity e) {
			return persistentDataPath + e.getNewPath () + "/" + newFishFolderNumber + "/" + e.getName () + ".png"; 
		}

		public string getExportPath(Entity e) {
			return persistentDataPath + e.getExportPath () + "/" + newFishFolderNumber + "/" + e.getName () + ".png"; 
		}
	}
}

