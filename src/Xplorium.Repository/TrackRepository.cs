namespace Xplorium.Repositories {
    using System;
    using System.Linq;

    using Xplorium.Models;

    public class TrackRepository : RepositoryBase {
        public int Count() {
            return DataContext.Tracks.Count();
        }

        public Track Get(Guid trackId) {
            try {
                return DataContext.Tracks.FirstOrDefault(track => track.TrackId == trackId);
            } catch {
                return null;
            }
        }

        public bool Create(Track newTrack, bool autoSave = true) {
            try {
                DataContext.Tracks.InsertOnSubmit(newTrack);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Modify(Track modifiedTrack, bool autoSave = true) {
            try {
                var originalTrack = Get(modifiedTrack.TrackId);
                DataContext.Tracks.Attach(modifiedTrack, originalTrack);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid trackId, bool autoSave = true) {
            try {
                var track = Get(trackId);
                DataContext.Tracks.DeleteOnSubmit(track);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}