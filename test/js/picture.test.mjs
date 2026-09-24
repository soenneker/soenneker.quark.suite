import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import test from "node:test";
import { runInNewContext } from "node:vm";
const script = readFileSync(new URL("../../src/Soenneker.Quark.Suite/wwwroot/js/picture.js", import.meta.url), "utf8");

test("theme changes select fixed picture sources without rewriting image URLs", () => {
  let dark = false;
  let callback;
  const element = (tagName, attributes) => ({
    tagName, nodeType: 1, attributes,
    getAttribute(name) { return this.attributes[name] ?? null; },
    setAttribute(name, value) { this.attributes[name] = value; },
    matches() { return true; }, querySelectorAll() { return []; }
  });
  const darkSource = element("SOURCE", { "data-quark-theme": "dark", "data-quark-media": "(max-width: 639px)", media: "(max-width: 639px) and (prefers-color-scheme: dark)", srcset: "/img/landing/product/inbox-workspace-mobile-dark.avif" });
  const lightSource = element("SOURCE", { "data-quark-theme": "light", "data-quark-media": "(max-width: 639px)", media: "(max-width: 639px)", srcset: "/img/landing/product/inbox-workspace-mobile.avif" });
  const img = element("IMG", { src: "/img/landing/product/inbox-workspace.avif" });
  img.parentElement = { querySelector: () => darkSource };
  const root = {
    nodeType: 1, classList: { contains: () => dark }, matches: () => false,
    querySelectorAll: () => [darkSource, lightSource]
  };
  runInNewContext(script, {
    document: { documentElement: root }, Node: { ELEMENT_NODE: 1 },
    MutationObserver: class { constructor(fn) { callback = fn; } observe() {} }
  });
  assert.equal(darkSource.attributes.media, "not all");
  assert.equal(lightSource.attributes.media, "(max-width: 639px)");
  dark = true;
  callback([{ type: "attributes", target: root, attributeName: "class" }]);
  assert.equal(darkSource.attributes.media, "(max-width: 639px)");
  assert.equal(lightSource.attributes.media, "not all");
  callback([{ type: "childList", addedNodes: [darkSource, lightSource] }]);
  lightSource.attributes.media = "all";
  callback([{ type: "attributes", target: lightSource, attributeName: "media" }]);
  assert.equal(lightSource.attributes.media, "not all");
  dark = false;
  callback([{ type: "attributes", target: root, attributeName: "class" }]);
  assert.equal(lightSource.attributes.media, "(max-width: 639px)");
  assert.equal(darkSource.attributes.media, "not all");
  assert.equal(img.attributes.src, "/img/landing/product/inbox-workspace.avif");
  assert.equal(lightSource.attributes.srcset, "/img/landing/product/inbox-workspace-mobile.avif");
  assert.equal(darkSource.attributes.srcset, "/img/landing/product/inbox-workspace-mobile-dark.avif");
});

