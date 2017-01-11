#!/bin/bash

# This script reads the name of the upstream branch at https://github.com/aspnet/MusicStore.git
# from a file called 'upstream-branch', then it merges this branch into the current branch.

UPSTREAM_BRANCH=$(cat upstream-branch)

if [[ -z "$UPSTREAM_BRANCH" ]]
then
  echo "Please add an upstream-branch file containing the upstream branch name"
  exit 1
fi

echo "Merging changes from upstream"

git remote add upstream https://github.com/aspnet/MusicStore.git 2>/dev/null || true
git fetch upstream
git merge "upstream/$UPSTREAM_BRANCH"

echo "You can push changes by executing 'git push'"
