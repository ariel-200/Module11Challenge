using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NailStudio.Pages
{
    // PageModel for the nail matching page
    public class NailMatchModel : PageModel
    {
        // This binds the form values (color, shape, design) to the model
        [BindProperty]
        public NailMatchViewModel NailPreferences { get; set; } = new NailMatchViewModel();

        // Text description of the match
        public string MatchResult { get; set; }

        // Image path for the matched nails (based on all 3 choices)
        public string MatchImagePath { get; set; }

        // View model for the nail matching form
        public class NailMatchViewModel
        {
            [Required(ErrorMessage = "Please select a color.")]
            public string Color { get; set; }

            [Required(ErrorMessage = "Please select a shape.")]
            public string Shape { get; set; }

            [Required(ErrorMessage = "Please select a design.")]
            public string DesignStyle { get; set; }
        }

        // Handles GET requests (when the page first loads)
        public void OnGet()
        {
            // No special setup needed on first load
        }

        // Handles POST requests (when the form is submitted)
        public void OnPost()
        {
            // Make sure all required options are selected
            if (!ModelState.IsValid)
            {
                // If validation fails, the page will re-render and show error messages
                return;
            }

            // Build the match result text and image path based on the selections
            BuildMatch(NailPreferences);
        }

        // Creates the match description and the image path
        private void BuildMatch(NailMatchViewModel prefs)
        {
            // Create a simple text description
            MatchResult = $"We matched you with {prefs.Color} {prefs.Shape} nails with a {prefs.DesignStyle.ToLower()} design.";

            // Create “slugs” for file names: lowercase and no spaces
            string colorSlug = prefs.Color.ToLowerInvariant();
            string shapeSlug = prefs.Shape.ToLowerInvariant();
            string designSlug = prefs.DesignStyle.ToLowerInvariant();

            // Example: /images/pink_coffin_simple.jpg
            MatchImagePath = $"/images/{colorSlug}_{shapeSlug}_{designSlug}.jpg";
        }
    }
}
