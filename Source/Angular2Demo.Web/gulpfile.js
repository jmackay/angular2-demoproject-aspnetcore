/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
	del = require("del"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	uglify = require("gulp-uglify"),
	flatten = require("gulp-flatten");

var paths = {
	webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.appFolderCompiledFileTypes = [paths.webroot + "app/**/*.js", paths.webroot + "app/**/*.js.map"];
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.nodeModulesJsDest = paths.webroot + "lib/node_modules/";

// You can use these to copy over typescript files from another project if you want to split your data layer from website (recommended for actual real world use!)
paths.dataDTS = "../**/*.cs.d.ts";
paths.localDts = "dts/*.cs.d.ts";
paths.localDtsDest = "dts/";


// Because systemjs and angular2 currently need to lazy load the files, we can't just copy over the ones we want, we need to copy over the whole folder structure into wwwroot... ugh!
// Once we can do that, we will need to also update our _layout.html script import locations!

paths.nodeModulesJs = [
	"node_modules/angular2/**/*",
	"node_modules/es6-shim/**/*",
	"node_modules/systemjs/**/*",
	"node_modules/rxjs/**/*",
	"node_modules/es6-promise/**/*",
	"node_modules/js-data/**/*",
	"node_modules/js-data-http/**/*",
	"node_modules/bootstrap/**/*",
	"node_modules/jquery/**/*",
];

gulp.task("clean:js", function () {
	return del([paths.concatJsDest]);
});

gulp.task("clean:libsjs", function () {
	return del([paths.nodeModulesJsDest]);
});

gulp.task("clean:app", function () {
	return del(paths.appFolderCompiledFileTypes);
})
gulp.task("clean:css", function () {
	return del([paths.concatCssDest]);
});

gulp.task("clean:localDTS", function () {
	return del([paths.localDts, "!*.extended.d.ts"]);
});

gulp.task("clean", ["clean:js", "clean:libsjs", "clean:css"]);

gulp.task("min:js", function () {
	return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
	return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

// Hopefully this method can die once angular2-cli is released, or a better way to "bundle" angular2.
gulp.task("angular2:moveLibs", ["clean:libsjs"],
	function () {
		return gulp.src(paths.nodeModulesJs, { base: "node_modules" })
			.pipe(gulp.dest(paths.nodeModulesJsDest));
	});


// This is only needed if you split your data layer from your web project (recommended for actual development, but left as single project for demo purposes)
// I'd recommend setting it up to run after build using Visual Studio / VSCode runners. You could also setup a watcher
gulp.task("build:copyDTS", ["clean:localDTS"], function () {
	return gulp.src(paths.dataDTS)
		.pipe(flatten())
		.pipe(gulp.dest(paths.localDtsDest));
});

gulp.task("build:dev", ["min", "build:copyDTS"]);

gulp.task("build:prod", ["min", "angular2:moveLibs"]);

